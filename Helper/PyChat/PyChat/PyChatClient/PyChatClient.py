from Forms.LoginRegisterWindow import LoginRegisterWindow
from Logic.SocketChatService import SocketChatService
from tkinter import Tk

def main():
    root = Tk()
    loginRegisterWindow = LoginRegisterWindow(root, SocketChatService('localhost',8000))
    root.mainloop()

main()