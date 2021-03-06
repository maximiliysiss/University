package com.application.library.app.UserContext;

import androidx.room.Entity;
import androidx.room.Ignore;
import androidx.room.PrimaryKey;

/**
 * Пользовательский контекст
 */
@Entity
public class UserContext {

    @PrimaryKey(autoGenerate = true)
    int id;

    /**
     * Токен доступа
     */
    String accessToken;
    /**
     * Токен обновления
     */
    String refreshToken;
    /**
     * Роль пользователя
     */
    Integer userRole;

    public UserContext() {
    }

    @Ignore
    public UserContext(String accessToken, String refreshToken, Integer userRole) {
        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
        this.userRole = userRole;
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

    public Integer getUserRole() {
        return userRole;
    }

    public void setUserRole(Integer userRole) {
        this.userRole = userRole;
    }
}
