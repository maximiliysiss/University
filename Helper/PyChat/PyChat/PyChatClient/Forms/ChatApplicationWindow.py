from tkinter import *
from tkinter.scrolledtext import ScrolledText
from Common.UserContext import UserContext

class ChatApplicationWindow:

    userContext = UserContext.getInstance()

    def __init__(self, master, service):
        self.master = master
        self.service = service
        self.minMessageId = 0
        self.firstLoad = False

        master.title("Chat Client")
        master.geometry("500x400")
        master.resizable(0,0)

        bottomPoint = 0.9
        bottomPointWithPadding = bottomPoint + 0.03

        self.chat = ScrolledText(master)
        self.chat.place(x=0,y=0,relheight=bottomPoint,relwidth=1)
        self.chatInput = Entry(master)
        self.chatInput.place(relx=0,rely=bottomPointWithPadding,relwidth=0.8)
        self.chatSend = Button(master, text="Send", command=self.sendMessageClick)
        self.chatSend.place(relx=0.81,rely=bottomPointWithPadding, relwidth=0.15)

    def newMessage(self, message):
        self.chat.insert(END, message["userName"] + ": " + message["message"] + '\n')

    def sendMessageClick(self):
        msg = self.chatInput.get()
        if len(msg) <= 0:
            return
        self.chatInput.delete(0, END)
        self.service.sendJsonString({"message":msg,"userid":self.userContext.userId,"userName":self.userContext.userName})

    def loadMessages(self, msgs, min):
        self.minMessageId = min
        for msg in reversed(msgs):
            self.chat.insert(END, msg + '\n')

        if not self.firstLoad:
            self.firstLoad = True
            self.chat.vbar.set(0.0, 1.0)

    def updateScrollbar(self, x, y, z):
        if not self.firstLoad:
            return
        print(x)