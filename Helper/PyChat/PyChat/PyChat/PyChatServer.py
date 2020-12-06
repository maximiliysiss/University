from Logic.SqlConnectionClient import SqlClient
from Logic.ChatServer import ChatServer
from Common.Crypt import Crypt
import json
  
# Запуск сервера
def main(): 
    # считаем настройки из файла
    with open('config.json') as f: 
        config = json.load(f)
        port = config["port"]
        ip = config["ip"]
        connString = config["connectionString"]
        key = config["key"]
        maxPool = config["maxPool"]

    Crypt.init(key)
    sqlClient = SqlClient(connString)
    server = ChatServer(ip, port, sqlClient, maxPool)
    server.start()

if __name__ == '__main__': 
    main()