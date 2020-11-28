import socket
from Logic.ChatClient import ChatClient
from _thread import *
import threading 

# Класс сервера
class ChatServer:
    def __init__(self, ip, port, sqlclient, maxPool):
        self.ip = ip
        self.port = port
        self.sqlclient = sqlclient
        self.listenSocket = None
        self.clients = []
        # Сколько разрешено пользователей
        self.maxPool = maxPool

    # Запуск сервера
    def start(self):
        self.listenSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM) 
        self.listenSocket.bind((self.ip, self.port))
        self.listenSocket.listen(5) 
        print("socket is listening")

        # Цикл принятия новых пользователей
        while True: 
            clientSocket, addr = self.listenSocket.accept()
            # Если места нету, то отправим ошибку входа
            if len(self.clients) + 1 > self.maxPool:
                clientSocket.send({"action":"loginfail","data":"Server is full"})
                clientSocket.close()
                continue

            # Добавление нового пользователя к списку и его запуск в отдельном
            # потоке
            print('Connected to :', addr[0], ':', addr[1]) 
            newClient = ChatClient(clientSocket, self.sqlclient, self)
            start_new_thread(newClient.start, ())
            self.clients.append(newClient)

        self.listenSocket.close()

    # Отправка всем пользователям JSON + его сохранение
    def broadcastJsonMessage(self, client, message):

        sendToClients = self.clients

        if "private" in message: 
            self.sqlclient.insertPrivateMessage(message)
            message["message"] = "[private] " + message["message"]
            sendToClients = list(filter(lambda x: x.user[0] == message["private"] or x == client, sendToClients))
        else:
            self.sqlclient.insertMessage(message)

        for cl in sendToClients:
            cl.sendJson(message)

    # Удаление пользователя из списка пользователей
    def removeFromClients(self, client):
        self.clients.remove(client)

    # Удаление пользователя и рассылка сообщение о его выходе
    def remove(self, client):
        self.clients.remove(client)
        self.broadcastJsonMessage(client, {"userName":client.user[1],"message": client.user[1] + " out", "userid": client.user[0]})