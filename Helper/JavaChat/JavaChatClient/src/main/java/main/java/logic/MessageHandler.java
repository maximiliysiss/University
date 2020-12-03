package main.java.logic;

import main.java.models.messages.Message;

public interface MessageHandler {
    void handle(Message message);
}
