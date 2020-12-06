from Crypto.Cipher import DES

# Класс для криптографии
class Crypt:
    __instance = None
    def __init__(self, key):
        if not Crypt.__instance:
            print(" __init__ method called..")
            self.key = key
            self.des = DES.new(key, DES.MODE_ECB)

    @classmethod
    def init(cls, key):
        if not cls.__instance:
            cls.__instance = Crypt(key)
        return cls.__instance

    @classmethod
    def getInstance(cls):
        return cls.__instance

    # Сделать строку кратной по длине 8
    def pad(self, text):
        while len(text) % 8 != 0:
            text += ' '
        return text

    # Зашифровать
    def encrypt(self, text):
        padded_text = self.pad(text).encode()
        return self.des.encrypt(padded_text)

    # Дешифровать
    def decrypt(self, text):
        return self.des.decrypt(text).decode().strip()
