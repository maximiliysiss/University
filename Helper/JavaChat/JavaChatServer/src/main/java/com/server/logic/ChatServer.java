package com.server.logic;

import com.server.data.SqlInteractor;

import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public class ChatServer extends Thread implements Serverable {
    private final String ip;
    private final int port;
    private final SqlInteractor sqlInteractor;
    private final int maxPool;

    private final List<ChatClient> chatClients = new ArrayList<>();

    public ChatServer(String ip, int port, SqlInteractor sqlInteractor, int maxPool) {
        this.ip = ip;
        this.port = port;
        this.sqlInteractor = sqlInteractor;
        this.maxPool = maxPool;
    }

    @Override
    public void run() {
        super.run();

        System.out.println("Try start server");

        try {
            InetAddress inetAddress = InetAddress.getByName(ip);
            ServerSocket serverSocket = new ServerSocket(port, 5, inetAddress);
            System.out.println("Server started");
            while (true) {
                Socket clientSocket = serverSocket.accept();
                if(chatClients.size() + 1 > maxPool){
                    clientSocket.close();
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    @Override
    public <T> void sendBroadcastJsonMessage(T message) {
        for(ChatClient chatClient: this.chatClients){
            chatClient.sendJson(message);
        }
    }

    @Override
    public <T> void sendPrivateJsonMessage(T message) {

    }
}
