from tkinter import *
from tkinter.scrolledtext import ScrolledText
from Common.UserContext import UserContext

# Окно чата
class ChatApplicationWindow:

    # Контекст пользователя
    userContext = UserContext.getInstance()

    def __init__(self, master, service):
        self.master = master
        self.service = service

        # Зададим размеры, заголовок и заблокируем изменения размера по кнопке
        master.title("Chat Client")
        master.geometry("500x400")
        master.resizable(0,0)

        bottomPoint = 0.9
        bottomPointWithPadding = bottomPoint + 0.03

        # Создание элемента чата, кнопки и ввода сообщения
        self.chat = ScrolledText(master, state=DISABLED)
        self.chat.place(x=0,y=0,relheight=bottomPoint,relwidth=1)
        self.chatInput = Entry(master)
        self.chatInput.place(relx=0,rely=bottomPointWithPadding,relwidth=0.8)
        self.chatSend = Button(master, text="Send", command=self.sendMessageClick)
        self.chatSend.place(relx=0.81,rely=bottomPointWithPadding, relwidth=0.15)

    # Обработка нового сообщения от сервера
    def newMessage(self, message):
        self.addLineToChat(message["userName"] + ": " + message["message"])

    # Отправка сообщения на сервер
    def sendMessageClick(self):
        msg = self.chatInput.get()
        # Если пусто, то отправлять не надо
        if len(msg) <= 0:
            return
        # Очистим ввод
        self.chatInput.delete(0, END)
        self.service.sendJsonString({"message":msg,"userid":self.userContext.userId,"userName":self.userContext.userName})

    # Загрузим сообщения в чат
    def loadMessages(self, msgs):
        self.addLineToChat('\n'.join(msgs))
        self.chat.see("end")

    # Если неправильный вход, то заблокировать все кнопки и вывести сообщение от сервера
    def errorLogin(self, msg):
        self.chatInput.configure(state=DISABLED)
        self.chatSend.configure(state=DISABLED)
        self.addLineToChat(msg)

    # Функция записи новой строки в чат.  Так как чат в режиме readonly, то
    # следует его разблокировать и снова заблокировать
    def addLineToChat(self, line):
        if len(line) == 0:
            return
        self.chat.configure(state="normal")
        self.chat.insert(END, line + '\n')
        self.chat.configure(state="disabled")