package com.server.models;

/**
 * Класс пользователя
 */
public class User {
    private String login;
    private int id;

    public User(String login, int id) {
        this.login = login;
        this.id = id;
    }

    public String getLogin() {
        return login;
    }

    public void setLogin(String login) {
        this.login = login;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
