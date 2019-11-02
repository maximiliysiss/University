package com.application.autostation.network.models.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Пользователь
 */
public class User {
    @SerializedName("login")
    @Expose
    String login;

    public String getLogin() {
        return login;
    }

    public void setLogin(String login) {
        this.login = login;
    }
}
