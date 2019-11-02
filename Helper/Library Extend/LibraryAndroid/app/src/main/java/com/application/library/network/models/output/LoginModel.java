package com.application.library.network.models.output;

/**
 * Модель входа
 */
public class LoginModel {
    String login;
    String password;

    public LoginModel(String login, String password) {
        this.login = login;
        this.password = password;
    }

    public String getLogin() {
        return login;
    }

    public void setLogin(String login) {
        this.login = login;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
