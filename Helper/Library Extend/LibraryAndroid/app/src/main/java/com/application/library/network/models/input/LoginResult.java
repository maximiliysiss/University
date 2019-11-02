package com.application.library.network.models.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Результат авторизации
 */
public class LoginResult {
    @SerializedName("userRole")
    @Expose
    private Integer userRole;
    /**
     * Токен доступа
     */
    @SerializedName("accessToken")
    @Expose
    private String accessToken;
    /**
     * Токен обновления
     */
    @SerializedName("refreshToken")
    @Expose
    private String refreshToken;

    public Integer getUserRole() {
        return userRole;
    }

    public void setUserRole(Integer userRole) {
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
}
