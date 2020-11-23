import socket
from Logic.ChatClient import ChatClient
from _thread import *
import threading 

class ChatServer:
    def __init__(self, ip, port, sqlclient, maxPool):
        self.ip = ip
        self.port = port
        self.sqlclient = sqlclient
        self.listenSocket = None
        self.clients = []
        self.maxPool = maxPool

    def start(self):
        self.listenSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM) 
        self.listenSocket.bind((self.ip, self.port))
        self.listenSocket.listen(5) 
        print("socket is listening")

        while True: 
            clientSocket, addr = self.listenSocket.accept()
            if len(self.clients) + 1 > self.maxPool:
                clientSocket.send({"message":"Server is full"})
                clientSocket.close()
                continue

            print('Connected to :', addr[0], ':', addr[1]) 
            newClient = ChatClient(clientSocket, self.sqlclient, self)
            start_new_thread(newClient.start, ())
            self.clients.append(newClient)

        self.listenSocket.close()

    def broadcastMessage(self, client, message):
        for cl in self.clients:
            cl.send(message)

    def broadcastJsonMessage(self, client, message):
        for cl in self.clients:
            cl.sendJson(message)

    def remove(self, client):
        self.clients.remove(client)
        self.broadcastJsonMessage(client, {"userName":client.user[1],"message": client.user[1] + " out"})