﻿import socket 
from _thread import *
import threading 
from Logic.ChatClient import ChatClient  
from Logic.SqlConnectionClient import SqlClient
  
def main(): 
    host = "localhost" 
    port = 8000

    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM) 
    s.bind((host, port)) 
    print("socket binded to port", port) 
  
    s.listen(5) 
    print("socket is listening") 
  
    sqlClient = SqlClient("Server=localhost,29516;Database=chatpy;Trusted_Connection=True;")

    while True: 
        c, addr = s.accept()
        print('Connected to :', addr[0], ':', addr[1]) 
        newClient = ChatClient(c, sqlClient)
        start_new_thread(newClient.start, ()) 
    s.close()   
  
if __name__ == '__main__': 
    main()