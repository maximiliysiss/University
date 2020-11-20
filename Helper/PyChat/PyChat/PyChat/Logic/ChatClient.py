import json

class ChatClient:
    def __init__(self, socket, sqlclient):
        self.socket = socket
        self.sqlclient = sqlclient

    def start(self):
        loginData = self.getJsonStringFromChannel()
        if loginData is None:
            print('Incorrect login')
            return
        print(loginData)

        loginJsonData = json.loads(loginData)
        self.sqlclient.tryLogin(loginJsonData)

        while True:
            print(self.getJsonStringFromChannel())
          
        self.socket.close()
        
    def getJsonStringFromChannel(self):
        data = self.socket.recv(2)
        if not data:
            print('Incorrect data')
            return None
        msgLength = int.from_bytes(data, "big")
        return self.socket.recv(msgLength).decode()