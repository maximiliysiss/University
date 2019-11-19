package com.application.carrepairandroid.application.UserContext;

import androidx.room.Entity;
import androidx.room.Ignore;
import androidx.room.PrimaryKey;

/**
 * Контекст пользователя
 */
@Entity
public class UserContext {

    @PrimaryKey(autoGenerate = true)
    public int id;

    /**
     * Токен доступа
     */
    public String accessToken;
    /**
     * Токен обновления
     */
    public String refreshToken;

    public UserContext() {
    }

    @Ignore
    public UserContext(String accessToken, String refreshToken) {
        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
    }

    public String getAccessToken() {
        return accessToken;
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
