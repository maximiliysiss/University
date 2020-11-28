from tkinter import *
from tkinter import ttk
from Forms.ChatApplicationWindow import ChatApplicationWindow
from Common.UserContext import UserContext

# Форма логина
class LoginRegisterWindow:
    def __init__(self, master, chatService):
        self.master = master
        self.service = chatService

        master.title("Login/Register")
        master.geometry("200x150")
        master.resizable(0,0)

        # Создание элементов окна
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
        self.registerButton = Button(self.frame, text="Register",command=self.registerClick)
        self.registerButton.pack()
        self.frame.pack()

    # Кнопка логин
    def loginClick(self):
        self.openChatWindowAndLogin(self.service.login)

    # Кнопка регистрации
    def registerClick(self):
        self.openChatWindowAndLogin(self.service.register)

    # Войти и открыть окно чата
    def openChatWindowAndLogin(self, loginAction):
        chatRoot = Tk()
        chatApplicationWindow = ChatApplicationWindow(chatRoot, self.service)
        self.service.onReceive = chatApplicationWindow.newMessage
        self.service.onLoadMessage = chatApplicationWindow.loadMessages
        self.service.onErrorLogin = chatApplicationWindow.errorLogin
        loginAction(self.loginTextBox.get(), self.passwordTextBox.get())
        UserContext.getInstance().userName = self.loginTextBox.get()
        self.master.destroy()
        chatRoot.mainloop()