import json

class ChatClient:
    def __init__(self, socket, sqlclient, server):
        self.socket = socket
        self.sqlclient = sqlclient
        self.server = server
        self.user = None

    def start(self):
        loginData = self.getJsonStringFromChannel()
        if loginData is None:
            print('Incorrect login')
            self.sendJson({"message":"Incorrect login data"})
            self.socket.close()
            return
        print(loginData)

        loginRegisterJsonData = json.loads(loginData)

        result = None
        if loginRegisterJsonData["action"] == "login":
            result = self.sqlclient.tryLogin(loginRegisterJsonData)
        else:
            result = self.sqlclient.tryregister(loginRegisterJsonData)

        if result is None:
            print("Error login/register")
            self.sendJson({"message":"Error login/password"})
            self.socket.close()
            return

        self.sendJson({"action":"login","data":result})
        self.user = self.sqlclient.loadUser(result)

        self.uploadData(0)
        self.server.broadcastJsonMessage(self, {"message":self.user[1] + " in", "userName":self.user[1]})

        while True:
            try:
                msg = self.getJsonStringFromChannel()

                jsonMsg = json.loads(msg)
                if "action" in jsonMsg:
                    self.handleAction(jsonMsg)
                    continue

                self.server.broadcastMessage(self, msg)
                self.sqlclient.insertMessage(jsonMsg)
                print(msg)
            except:
                print("Connection closed")
                break

        self.server.remove(self)
        self.socket.close()

    def convertMessageDTOToTransfer(self, msg):
        return {"message":msg[1], "id": msg[0]}
        
    def getJsonStringFromChannel(self):
        data = self.socket.recv(2)
        if not data:
            print('Incorrect data')
            return None
        msgLength = int.from_bytes(data, "big")
        return self.socket.recv(msgLength).decode()

    def send(self, msg):
        self.socket.send(len(msg).to_bytes(2, byteorder ="big"))
        self.socket.send(msg.encode())

    def sendJson(self, jsonMsg):
        msg = json.dumps(jsonMsg)
        self.send(msg)

    def uploadData(self, index):
        loadedMsg = map(self.convertMessageDTOToTransfer, self.sqlclient.loadMessagePage(index))
        self.sendJson({"action":"load", "data": list(loadedMsg)})

    def handleAction(self, jsonMsg):
        if jsonMsg["action"] == "load":
            self.uploadData(jsonMsg["index"])