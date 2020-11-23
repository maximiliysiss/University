import pyodbc
import hashlib

class SqlClient:
    def __init__(self, connString):
        self.connString = connString
        print("Connection String = ", connString)

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

    def loadUser(self, userId):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        data = cursor.execute("select Id, Login from [dbo].[Users] where Id = ?", userId).fetchone()
        cursor.close()
        return data

    def insertMessage(self, messageData):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        cursor.execute("insert into [dbo].[Messages](UserId, Content) values(?,?)", messageData["userid"], messageData["message"])
        conn.commit()
        cursor.close()

    def loadMessagePage(self, page ):
        conn = pyodbc.connect(self.connString)
        cursor = conn.cursor()
        data = cursor.execute("exec sp_messages ?", page).fetchall()
        cursor.close()
        return data