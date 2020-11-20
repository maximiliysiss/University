from tkinter import *
from tkinter import ttk
from Forms.ChatApplicationWindow import ChatApplicationWindow

class LoginRegisterWindow:
    def __init__(self, master, chatService):
        self.master = master
        self.service = chatService
        master.title("Login/Register")
        master.geometry("200x150")
        master.resizable(0,0)

        self.frame = ttk.Frame(master)

        self.loginLabel = Label(self.frame, text="Login")
        self.loginLabel.pack()
        self.loginTextBox = Entry(self.frame)
        self.loginTextBox.pack()
        self.passwordLabel = Label(self.frame, text="Password")
        self.passwordLabel.pack()
        self.passwordTextBox = Entry(self.frame)
        self.passwordTextBox.pack()
        self.loginButton = Button(self.frame, text="Login",command=self.loginClick)
        self.loginButton.pack()
        self.registerButton = Button(self.frame, text="Register")
        self.registerButton.pack()

        self.frame.pack()

    def loginClick(self):
        self.service.login(self.loginTextBox.get(), self.passwordTextBox.get())
        self.openChatWindow()

    def registerClick(self):
        self.service.register(self.loginTextBox.get(), self.passwordTextBox.get())
        self.openChatWindow()

    def openChatWindow(self):
        chatRoot = Tk()
        chatApplicationWindow = ChatApplicationWindow(chatRoot, self.service)
        self.master.destroy()
        chatRoot.mainloop()