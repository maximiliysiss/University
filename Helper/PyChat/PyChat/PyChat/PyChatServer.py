from Logic.SqlConnectionClient import SqlClient
from Logic.ChatServer import ChatServer
  
# Запуск сервера
def main(): 
    print("Enter host: ")
    host = input() 
    print("Enter port: ")
    port = int(input())

    sqlClient = SqlClient("Server=localhost,29516;Database=chatpy;UID=develop;PWD=root;DRIVER={ODBC Driver 17 for SQL Server}")
    server = ChatServer(host, port, sqlClient, 40)
    server.start()

if __name__ == '__main__': 
    main()