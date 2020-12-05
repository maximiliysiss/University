package com.server.data;

import com.server.models.Message;

public interface SqlInteractor {
    Integer tryLogin(String login, String password);
    Integer tryRegister(String login, String password);

    Integer getUserByName(String name);

    void insertMessage(Message message);

    void insertPrivateMessage(Message message, Integer userId);

    void clearData();

    String[] loadMessages(int id);
}
