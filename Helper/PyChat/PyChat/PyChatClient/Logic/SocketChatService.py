import socket
import json
from _thread import *
import threading 
from Common.UserContext import UserContext
from Common.Crypt import Crypt

# Класс-сервис для обработки запросов от сервака и отправка их на сервер
class SocketChatService:
    def __init__(self, host, port):
        print("Create SocketChatService")
        self.host = host
        self.port = port
        self.socket = None
        self.onReceive = None
        self.onLoadMessage = None
        self.onErrorLogin = None

    # Открытие канала к серверу
    def initConnection(self):
        if self.socket is not None:
            return
        self.socket = socket.socket(socket.AF_INET,socket.SOCK_STREAM) 
        self.socket.connect((self.host,self.port))
        # Получение собщений будет происходить в другом потоке
        start_new_thread(self.receiveMessage, ())

    # Отправить JSON на сервер
    def sendJsonString(self, jsonData):
        jsonString = json.dumps(jsonData)
        msgBytes = Crypt.getInstance().encrypt(jsonString)
        self.socket.send(len(msgBytes).to_bytes(2, byteorder ="big"))
        self.socket.send(msgBytes)

    # Попытка авторизации через сервер
    def login(self, login, password):
        self.initConnection()
        self.sendJsonString({"action":"login", "login":login, "password":password})

    # Попытка регистрации
    def register(self, login, password):
        self.initConnection()
        self.sendJsonString({"action":"register", "login":login, "password":password})
    
    # Обработка полученных сообщений
    def receiveMessage(self):
        while True:
            messageLength = int.from_bytes(self.socket.recv(2), "big")
            if messageLength is None:
                break
            if messageLength <= 0:
                continue
            msgData = self.socket.recv(messageLength)
            msg = Crypt.getInstance().decrypt(msgData)

            dataJson = json.loads(msg)
            # Если это действие, а не сообщений
            # Есть 2 типа посылок от сервера.  Это сообщения других
            # пользователей
            # и действия (технические сообщения от сервера)
            if "action" in dataJson:
                self.handleAction(dataJson)
                continue

            # Если пользователь определил, что нужно дополнительно обработать
            # сообщение
            if self.onReceive is None:
                continue
            self.onReceive(dataJson)

    # Обработка технических сообщений
    def handleAction(self, actionMsg):

        action = actionMsg["action"]

        # Если результат входа, то надо сохранить ID пользователя
        if action == "login":
            UserContext.getInstance().userId = actionMsg["data"]
        # Если прямая подгрузка сообщений, то надо их сохранить в чат
        elif action == "load":
            if self.onLoadMessage is not None:
                self.onLoadMessage(list(map(lambda x: x["message"],actionMsg["data"])))
        # Если пришла ошибка входа в систему
        elif action == "loginfail":
            if self.onErrorLogin is not None:
                self.onErrorLogin(actionMsg["data"])

