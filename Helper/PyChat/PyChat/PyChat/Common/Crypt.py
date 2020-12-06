from Crypto.Cipher import DES

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

    def pad(self, text):
        while len(text) % 8 != 0:
            text += ' '
        return text

    def encrypt(self, text):
        padded_text = self.pad(text).encode()
        return self.des.encrypt(padded_text)

    def decrypt(self, text):
        return self.des.decrypt(text).decode().strip()
