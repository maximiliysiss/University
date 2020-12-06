from Forms.LoginRegisterWindow import LoginRegisterWindow
from Logic.SocketChatService import SocketChatService
from tkinter import Tk
from Common.Crypt import Crypt
import json

def main():
    # считаем настройки из файла
    with open('config.json') as f: 
        config = json.load(f)
        port = config["port"]
        ip = config["ip"]
        key = config["key"]

    Crypt.init(key)
    root = Tk()
    loginRegisterWindow = LoginRegisterWindow(root, SocketChatService(ip,port))
    root.mainloop()

main()