# Контекст текущего пользователя.  Singleton, который хранит UserId, UserName
class UserContext:
    __instance = None
    def __init__(self):
        if not UserContext.__instance:
            print(" __init__ method called..")
            self.userId = None
            self.userName = None

    @classmethod
    def getInstance(cls):
        if not cls.__instance:
            cls.__instance = UserContext()
        return cls.__instance
