package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class LoginResult {

    @SerializedName("token")
    @Expose
    private String token;
    @SerializedName("userType")
    @Expose
    private Integer userType;

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public Integer getUserType() {
        return userType;
    }

    public void setUserType(Integer userType) {
        this.userType = userType;
    }
}
