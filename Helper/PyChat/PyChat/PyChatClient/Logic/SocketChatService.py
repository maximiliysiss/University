import socket

class SocketChatService:
    def __init__(self, host, port):
        print("Create SocketChatService")
        self.host = host
        self.port = port
        self.socket = None

    def initConnection(self):
        if self.socket is not None:
            return

        self.socket = socket.socket(socket.AF_INET,socket.SOCK_STREAM) 
        self.socket.connect((self.host,self.port))

    def sendJsonString(self, jsonString):
        self.socket.send(len(jsonString).to_bytes(2, byteorder ='big'))
        self.socket.send(jsonString.encode())

    def login(self, login, password):
        self.initConnection()
        self.sendJsonString(str({'action':'login', 'login':login, 'password':password}))

    def register(self, login, password):
        self.initConnection()
        self.sendJsonString(str({'action':'register', 'login':login, 'password':password}))
