from tkinter import *

class ChatApplicationWindow:
    def __init__(self, master, service):
        self.master = master
        self.service = service

        master.title("Chat Client")
        master.geometry("500x400")
        master.resizable(0,0)

        bottomPoint = 0.9
        bottomPointWithPadding = bottomPoint + 0.03

        self.chat = Text(master,state=DISABLED)
        self.chat.place(x=0,y=0,relheight=bottomPoint,relwidth=1)
        self.chatInput = Entry(master)
        self.chatInput.place(relx=0,rely=bottomPointWithPadding,relwidth=0.8)
        self.chatSend = Button(master, text="Send")
        self.chatSend.place(relx=0.81,rely=bottomPointWithPadding, relwidth=0.15)
