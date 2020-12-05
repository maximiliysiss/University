package com.server.logic;

import com.server.data.SqlInteractor;
import com.server.models.ActionMessage;
import com.server.models.Message;
import com.server.models.factory.Actions;

import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

public class ChatServer extends Thread implements Serverable {
    private final String ip;
    private final int port;
    private final SqlInteractor sqlInteractor;
    private final int maxPool;

    private final List<ChatClient> chatClients = new ArrayList<>();
    private ServerSocket serverSocket;
    private boolean isClose = false;

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
            serverSocket = new ServerSocket(port, 5, inetAddress);
            System.out.println("Server started");
            while (true) {
                Socket clientSocket = serverSocket.accept();
                System.out.println("New user in");
                ChatClient chatClient = new ChatClient(clientSocket, sqlInteractor, this);
                if (chatClients.size() + 1 > maxPool) {
                    chatClient.sendJson(Actions.ServerIsFull);
                    chatClient.close();
                    continue;
                }
                chatClients.add(chatClient);
                chatClient.start();
            }
        } catch (IOException e) {
            if (!isClose)
                e.printStackTrace();
        }

    }

    @Override
    public <T> void sendBroadcastJsonMessage(T message) {
        for (ChatClient chatClient : this.chatClients) {
            chatClient.sendJson(message);
        }
    }

    @Override
    public <T> void sendPrivateJsonMessage(T message, final Integer id) {
        Optional<ChatClient> chatClient = chatClients.stream().filter(x -> x.getUserId() == id).findFirst();
        if (chatClient.isPresent()) {
            chatClient.get().sendJson(message);
        }
    }

    @Override
    public void logout(ChatClient chatClient) {
        chatClients.remove(chatClient);
        onlineUpdate();
    }

    public void onlineUpdate() {
        sendBroadcastJsonMessage(new ActionMessage("online").packBody(chatClients.stream().map(x -> x.getUserName()).collect(Collectors.toList())));
    }

    public void shutdown() {
        try {
            isClose = true;
            serverSocket.close();
        } catch (IOException e) {
        }
    }
}
