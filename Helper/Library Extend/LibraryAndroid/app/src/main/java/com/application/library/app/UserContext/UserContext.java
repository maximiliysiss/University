package com.application.library.app.UserContext;

import androidx.room.Entity;
import androidx.room.Ignore;
import androidx.room.PrimaryKey;

/**
 * Информация о пользователе
 */
@Entity
public class UserContext {

    /**
     * ID
     */
    @PrimaryKey(autoGenerate = true)
    int id;

    /**
     * Токен для доступа
     */
    String accessToken;
    /**
     * Токен для обновления токенов (он хранится намного долго)
     */
    String refreshToken;
    /**
     * Роль пользователя
     */
    Integer userRole;

    public UserContext() {
    }

    /**
     * Конструктор
     * @param accessToken
     * @param refreshToken
     * @param userRole
     */
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
