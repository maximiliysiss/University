from Forms.LoginRegisterWindow import LoginRegisterWindow
from Logic.SocketChatService import SocketChatService
from tkinter import Tk

def main():
    print("Enter ip: ")
    host = input()
    print("Enter port: ")
    port = int(input())

    root = Tk()
    loginRegisterWindow = LoginRegisterWindow(root, SocketChatService(host,port))
    root.mainloop()

main()