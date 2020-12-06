import pyodbc
import hashlib

# Подключение к БД
class SqlClient:
    def __init__(self, connString):
        self.connString = connString
        print("Connection String = ", connString)

    # Попытка получить пользователя по логину и паролю
    def tryLogin(self, loginDataJson):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        passwordHash = hashlib.md5(loginDataJson["password"].encode()).hexdigest()

        data = cursor.execute("select Id from [dbo].[Users] where Login = ? and PasswordHash = ?", loginDataJson["login"], passwordHash).fetchall()
        cursor.close()

        if len(data) == 0:
            print("Cannot found user in db")
            return None

        return data[0][0]
     
    # Попытка создать нового пользователя
    def tryregister(self, registerDataJson):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        passwordHash = hashlib.md5(registerDataJson["password"].encode()).hexdigest()

        data = cursor.execute("select count(1) from [dbo].[Users] where Login = ? and PasswordHash = ?", registerDataJson["login"], passwordHash).fetchall()
        if data[0][0] > 0:
            print("User is founded yet")
            return None
        cursor.execute("insert into [dbo].[Users](Login,PasswordHash) values (?,?)", registerDataJson["login"], passwordHash)
        data = cursor.execute("select Id from [dbo].[Users] where Login = ? and PasswordHash = ?", registerDataJson["login"], passwordHash).fetchall()
        conn.commit()
        cursor.close()
        return data[0][0]

    # Получение информации о пользователе по ID
    def loadUser(self, userId):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        data = cursor.execute("select Id, Login from [dbo].[Users] where Id = ?", userId).fetchone()
        cursor.close()
        return data

    # Добавление нового пользователя
    def insertMessage(self, messageData):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        cursor.execute("insert into [dbo].[Messages](UserId, Content) values(?,?)", messageData["userid"], messageData["message"])
        conn.commit()
        cursor.close()

    def insertPrivateMessage(self, messageData):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        cursor.execute("exec sp_create_privatemessage @userId=?, @content=?, @receiveId=?", messageData["userid"], messageData["message"], messageData["private"])
        conn.commit()
        cursor.close()

    def loadMessageHistory(self, page, userId):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        data = cursor.execute("exec sp_messages @id=?, @userId=?", page, userId).fetchall()
        cursor.close()
        return data

    def getUserIdByLogin(self, login):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        data = cursor.execute("select Id from [dbo].[Users] where Login = ?", login).fetchone()
        cursor.close()
        return data