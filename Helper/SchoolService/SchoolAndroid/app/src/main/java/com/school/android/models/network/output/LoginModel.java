package com.school.android.models.network.output;

public class LoginModel {
    String login;
    String password;

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

    public LoginModel(String login, String password) {
        this.login = login;
        this.password = password;
    }
}
