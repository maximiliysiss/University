package com.example.chatjavaclient.chatsocket;

import com.example.chatjavaclient.ChatActivity;

import java.io.DataInputStream;
import java.io.IOException;
import java.net.Socket;

public class ReceiverSocket extends Thread {

    Socket socket;
    DataInputStream streamReader;

    ChatActivity chatActivity;

    public ReceiverSocket(Socket socket, ChatActivity chatActivity) {
        this.socket = socket;
        this.chatActivity = chatActivity;
    }

    @Override
    public void run() {
        super.run();

        try {
            streamReader = new DataInputStream(socket.getInputStream());

            while(true){
                int len = streamReader.readInt();
                byte[] data = new byte[len];
                streamReader.read(data);

                chatActivity.addMessage(String.valueOf(data));
            }

        } catch (IOException e) {
            e.printStackTrace();
        }

    }
}
