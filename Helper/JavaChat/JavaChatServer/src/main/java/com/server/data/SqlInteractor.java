package com.server.data;

import com.server.models.Message;

import java.util.List;

public interface SqlInteractor {
    Integer tryLogin(String login, String password);
    Integer tryRegister(String login, String password);

    Integer getUserByName(String name);

    void insertMessage(Message message);

    void insertPrivateMessage(Message message, Integer userId);

    void clearData();

    List<String> loadMessages(int id);
}
