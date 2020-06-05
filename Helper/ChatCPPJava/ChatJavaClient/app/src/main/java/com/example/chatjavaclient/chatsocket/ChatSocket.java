package com.example.chatjavaclient.chatsocket;

import com.example.chatjavaclient.ChatActivity;

import java.io.IOException;
import java.io.OutputStream;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.util.ArrayDeque;
import java.util.Queue;

public class ChatSocket extends Thread {

    private String userName;
    private Socket socket;
    private String ip;
    private int port;

    private OutputStream streamWriter;

    private ReceiverSocket receiverSocket;
    private ChatActivity chatActivity;
    private Queue<String> messages = new ArrayDeque<>();

    public ChatSocket(String userName, String ip, int port, ChatActivity chatActivity) {
        this.userName = userName;
        this.ip = ip;
        this.port = port;
        this.chatActivity = chatActivity;
    }

    @Override
    public void run() {
        super.run();

        try {

            socket = new Socket(ip, port);

            streamWriter = socket.getOutputStream();

            streamWriter.write(ByteBuffer.allocate(4).putInt(userName.length() * 2).array());
            streamWriter.flush();
            streamWriter.write(userName.getBytes(StandardCharsets.UTF_16BE));
            streamWriter.flush();

            chatActivity.addMessage("Welcome, " + userName);

            receiverSocket = new ReceiverSocket(socket, chatActivity);
            receiverSocket.start();

            while (true) {
                synchronized (messages) {
                    messages.wait();

                    String msg = messages.poll();

                    streamWriter.write(ByteBuffer.allocate(4).putInt(msg.length()).array());
                    streamWriter.flush();
                    streamWriter.write(msg.getBytes(StandardCharsets.UTF_16BE));
                    streamWriter.flush();

                    chatActivity.addMessage(msg);
                }
            }

        } catch (IOException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void interrupt() {
        super.interrupt();

        try {
            receiverSocket.interrupt();
            if (socket.isConnected())
                socket.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void sendMessage(String msg) {

        synchronized (messages) {
            messages.add(new StringBuilder().append(userName).append(": ").append(msg).toString());
            messages.notify();
        }

    }
}
