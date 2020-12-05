package com.server.logic;

public interface Serverable {
    <T> void sendBroadcastJsonMessage(T message);
    <T> void sendPrivateJsonMessage(T message, Integer id);
}
