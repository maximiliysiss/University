package com.server.models;

public class Message {
    private String message;
    private int senderId;
    private String sender;

    public Message(String message, int senderId, String sender) {
        this.message = message;
        this.senderId = senderId;
        this.sender = sender;
    }

    public Message() {
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public int getSenderId() {
        return senderId;
    }

    public void setSenderId(int senderId) {
        this.senderId = senderId;
    }

    public String getSender() {
        return sender;
    }

    public void setSender(String sender) {
        this.sender = sender;
    }
}
