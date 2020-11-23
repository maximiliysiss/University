import json

class ChatClient:
    def __init__(self, socket, sqlclient, server):
        self.socket = socket
        self.sqlclient = sqlclient
        self.server = server

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
            self.sendJson({"message":"Error login/password"})
            self.socket.close()
            return

        while True:
            msg = self.getJsonStringFromChannel()
            print(msg)
            self.server.broadcastMessage(self, msg)
          
        self.socket.close()
        
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