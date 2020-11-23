import socket
import json
from _thread import *
import threading 

class SocketChatService:
    def __init__(self, host, port):
        print("Create SocketChatService")
        self.host = host
        self.port = port
        self.socket = None
        self.onReceive = None

    def initConnection(self):
        if self.socket is not None:
            return

        self.socket = socket.socket(socket.AF_INET,socket.SOCK_STREAM) 
        self.socket.connect((self.host,self.port))
        start_new_thread(self.receiveMessage, ())

    def sendJsonString(self, jsonData):
        jsonString = json.dumps(jsonData)
        self.socket.send(len(jsonString).to_bytes(2, byteorder ="big"))
        self.socket.send(jsonString.encode())

    def login(self, login, password):
        self.initConnection()
        self.sendJsonString({"action":"login", "login":login, "password":password})

    def register(self, login, password):
        self.initConnection()
        self.sendJsonString({"action":"register", "login":login, "password":password})

    def receiveMessage(self):
        while True:
            messageLength = int.from_bytes(self.socket.recv(2), "big")
            if messageLength is None:
                break
            if messageLength <= 0:
                continue
            msg = self.socket.recv(messageLength).decode()
            if self.onReceive is None:
                continue
            self.onReceive(msg)