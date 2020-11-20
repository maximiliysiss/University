
class ChatClient:
    def __init__(self, socket):
        self.socket = socket

    def start(self):
        loginData = self.getJsonStringFromChannel()
        if loginData is None:
            print('Incorrect login')
            return
        print(loginData)

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