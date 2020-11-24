import json

# Клиент сервера
class ChatClient:
    def __init__(self, socket, sqlclient, server):
        self.socket = socket
        self.sqlclient = sqlclient
        self.server = server
        self.user = None

    # Запуск клиента (делается в отдельном потоке)
    def start(self):
        # Получение первого сообщения с данными входа
        loginData = self.getJsonStringFromChannel()
        # Если ничего нет, то это ошибка
        if loginData is None:
            print('Incorrect login')
            self.sendJson({"action":"loginfail","data":"Incorrect login data"})
            self.server.removeFromClients(self)
            self.socket.close()
            return
        print(loginData)

        loginRegisterJsonData = json.loads(loginData)

        # Попытка авторизоваться или зарегистрироваться
        result = None
        if loginRegisterJsonData["action"] == "login":
            result = self.sqlclient.tryLogin(loginRegisterJsonData)
        else:
            result = self.sqlclient.tryregister(loginRegisterJsonData)

        # Если неудача, то отправим пользователю ошибку
        if result is None:
            print("Error login/register")
            self.sendJson({"action":"loginfail","data":"Error login/password"})
            self.socket.close()
            self.server.removeFromClients(self)
            return

        # Если все хорошо, то загрузим данные пользователя и отправим их ему
        self.sendJson({"action":"login","data":result})
        self.user = self.sqlclient.loadUser(result)

        # Отправим также пользователю историю сообщений и сообщение о удачном входе
        self.uploadMessageHistory()
        self.server.broadcastJsonMessage(self, {"message":self.user[1] + " in", "userName":self.user[1], "userid":self.user[0]})

        # Получем сообщения от пользователя, отправляем их всем и добавляем в БД
        while True:
            try:
                msg = self.getJsonStringFromChannel()
                jsonMsg = json.loads(msg)
                self.server.broadcastJsonMessage(self, jsonMsg)
                print(msg)
            except:
                print("Connection closed")
                break

        # Удалим пользователя из списка и закроем его сокет
        self.server.remove(self)
        self.socket.close()

    # Конвертация строки из БД в JSON
    def convertMessageDTOToTransfer(self, msg):
        return {"message":msg[1], "id": msg[0]}
    
    # Получение сообщения из канала связи. Сначала идет длина сообщения, потом само сообщение
    def getJsonStringFromChannel(self):
        data = self.socket.recv(2)
        if not data:
            print('Incorrect data')
            return None
        msgLength = int.from_bytes(data, "big")
        return self.socket.recv(msgLength).decode()

    # Отправить сообщение пользователю
    def send(self, msg):
        self.socket.send(len(msg).to_bytes(2, byteorder ="big"))
        self.socket.send(msg.encode())

    # Отправить JSON пользователю
    def sendJson(self, jsonMsg):
        msg = json.dumps(jsonMsg)
        self.send(msg)

    # Выгрузить пользователю историю сообщений
    def uploadMessageHistory(self):
        loadedMsg = map(self.convertMessageDTOToTransfer, self.sqlclient.loadMessageHistory())
        self.sendJson({"action":"load", "data": list(loadedMsg)})