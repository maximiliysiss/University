package com.server.logic;

public interface Serverable {
    void sendBroadcastMessage(String message);
    void sendPrivateMessage();
}
