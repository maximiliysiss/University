package com.server.logic;

import com.server.data.SqlInteractor;
import com.server.models.ActionMessage;
import com.server.models.factory.Actions;

import java.io.IOException;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Сервер
 */
public class ChatServer extends Thread implements Serverable {
    private final String ip;
    private final int port;
    /**
     * Максимальное количество подключений
     */
    private final int maxPool;

    /**
     * Подключение к БД
     */
    private final SqlInteractor sqlInteractor;

    /**
     * Клиенты
     */
    private final List<ChatClient> chatClients = new ArrayList<>();

    /**
     * Сокет
     */
    private ServerSocket serverSocket;
    /**
     * Закрыто ли приложение
     */
    private boolean isClose = false;

    public ChatServer(String ip, int port, SqlInteractor sqlInteractor, int maxPool) {
        this.ip = ip;
        this.port = port;
        this.sqlInteractor = sqlInteractor;
        this.maxPool = maxPool;
    }

    /**
     * Запуск потока
     */
    @Override
    public void run() {
        super.run();

        System.out.println("Try start server " + ip + ":" + port);

        try {
            InetAddress inetAddress = InetAddress.getByName(ip);
            serverSocket = new ServerSocket(port, 5, inetAddress);
            System.out.println("Server started");
            while (true) {
                Socket clientSocket = serverSocket.accept();
                ChatClient chatClient = new ChatClient(clientSocket, sqlInteractor, this);
                if (chatClients.size() + 1 > maxPool) {
                    chatClient.sendJson(Actions.ServerIsFull);
                    chatClient.close();
                    continue;
                }
                chatClient.start();
            }
        } catch (IOException e) {
            if (!isClose)
                e.printStackTrace();
        }

    }

    /**
     * Отправить сообщение всем
     *
     * @param message
     * @param <T>
     */
    @Override
    public <T> void sendBroadcastJsonMessage(T message) {
        for (ChatClient chatClient : this.chatClients) {
            chatClient.sendJson(message);
        }
    }

    /**
     * Отправить сообщение определенным пользователям
     *
     * @param message
     * @param ids
     * @param <T>
     */
    @Override
    public <T> void sendPrivateJsonMessage(T message, final List<Integer> ids) {
        List<ChatClient> receivers = chatClients.stream().filter(x -> ids.contains(x.getUserId())).collect(Collectors.toList());
        for (ChatClient chatClient : receivers)
            chatClient.sendJson(message);
    }

    @Override
    public void login(ChatClient chatClient) {
        System.out.println("New user");
        chatClients.add(chatClient);
    }

    /**
     * Выход пользователя
     *
     * @param chatClient
     */
    @Override
    public void logout(ChatClient chatClient) {
        System.out.println("Sign out user");
        chatClients.remove(chatClient);
        onlineUpdate();
    }

    /**
     * Отправка пользователям актуального списка пользователей
     */
    public void onlineUpdate() {
        sendBroadcastJsonMessage(new ActionMessage("online").packBody(chatClients.stream().map(x -> x.getUserName()).collect(Collectors.toList())));
    }

    /**
     * Обновить у всех историю
     */
    @Override
    public void clearDataAction() {
        sqlInteractor.clearData();
        for (ChatClient chatClient : chatClients) {
            chatClient.loadData();
        }
    }

    /**
     * Есть ли уже такой пользователь в онлайне
     *
     * @param id
     * @return
     */
    @Override
    public boolean isExists(int id) {
        return chatClients.stream().filter(x -> x.getUserId() == id).count() > 0;
    }

    /**
     * Вырубить сервер
     */
    public void shutdown() {
        try {
            isClose = true;
            serverSocket.close();
        } catch (IOException e) {
        }
    }
}
