package com.server.models;

import com.google.gson.Gson;

/**
 * Смотри на клиент
 */
public class ActionMessage implements Messagable {

    private static Gson gson = new Gson();

    private String action;
    private String data;

    public ActionMessage() {
    }

    public ActionMessage(String action, String data) {
        this.action = action;
        this.data = data;
    }

    public ActionMessage(String action) {
        this.action = action;
    }

    public String getAction() {
        return action;
    }

    public void setAction(String action) {
        this.action = action;
    }

    public String getData() {
        return data;
    }

    public void setData(String data) {
        this.data = data;
    }

    public <T> T getBody(Class<T> tClass) {
        return gson.fromJson(data, tClass);
    }

    public <T> ActionMessage packBody(T obj) {
        data = gson.toJson(obj);
        return this;
    }
}
