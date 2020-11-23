from Logic.SqlConnectionClient import SqlClient
from Logic.ChatServer import ChatServer
  
def main(): 
    host = "localhost" 
    port = 8000

    sqlClient = SqlClient("Server=localhost,29516;Database=chatpy;UID=develop;PWD=root;DRIVER={ODBC Driver 17 for SQL Server}")
    server = ChatServer("localhost", 8000, sqlClient, 40)
    server.start()

if __name__ == '__main__': 
    main()