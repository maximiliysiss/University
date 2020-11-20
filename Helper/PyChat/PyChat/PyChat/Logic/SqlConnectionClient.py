import sqlite3
import hashlib

class SqlClient:
    def __init__(self, connString):
        self.connString = connString
        print("Connection String = ", connString)

    def tryLogin(self, loginDataJson):
        conn = sqlite3.connect(self.connString)
        cursor = conn.cursor()

        passwordHash = hashlib.md5(loginDataJson["password"].encode())

        data = cursor.execute("select count(1) from [dbo].[Users] where Login = ? and PasswordHash = ?", [loginDataJson["login"], passwordHash])
        print(data)
