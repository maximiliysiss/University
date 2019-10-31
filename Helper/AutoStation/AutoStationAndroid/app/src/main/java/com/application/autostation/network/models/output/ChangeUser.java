package com.application.autostation.network.models.output;

public class ChangeUser {
    String login;
    String password;
    String passwordConfirm;

    public ChangeUser(String login, String password, String passwordConfirm) {
        this.login = login;
        this.password = password;
        this.passwordConfirm = passwordConfirm;
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

    public String getPasswordConfirm() {
        return passwordConfirm;
    }

    public void setPasswordConfirm(String passwordConfirm) {
        this.passwordConfirm = passwordConfirm;
    }
}
