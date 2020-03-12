package com.example.testangryandroid.app.UserContext;

import androidx.room.Entity;
import androidx.room.Ignore;
import androidx.room.PrimaryKey;

import com.example.testangryandroid.network.models.LoginResult;

/**
 * Контекст пользователя
 */
@Entity
public class UserContext {

    private String name;
    /**
     * Токен доступа
     */
    String accessToken;
    /**
     * Токен обновления
     */
    String refreshToken;

    public UserContext() {
    }

    @Ignore
    public UserContext(String accessToken, String refreshToken) {
        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
    }

    public UserContext(LoginResult loginResult) {
        this.accessToken = loginResult.getAccessToken();
        this.refreshToken = loginResult.getRefreshToken();
        this.name = loginResult.getName();
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getAccessToken() {
        return "Bearer " + accessToken;
    }

    public void setAccessToken(String accessToken) {
        this.accessToken = accessToken;
    }

    public String getRefreshToken() {
        return refreshToken;
    }

    public void setRefreshToken(String refreshToken) {
        this.refreshToken = refreshToken;
    }
}
