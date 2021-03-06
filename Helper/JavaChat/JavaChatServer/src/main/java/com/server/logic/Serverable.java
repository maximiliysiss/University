package com.server.logic;

import java.util.List;

/**
 * Интерфейс сервера
 */
public interface Serverable {
    <T> void sendBroadcastJsonMessage(T message);
    <T> void sendPrivateJsonMessage(T message, final List<Integer> ids);

    void login(ChatClient chatClient);
    void logout(ChatClient chatClient);
    void onlineUpdate();

    void clearDataAction();

    boolean isExists(int id);

}
