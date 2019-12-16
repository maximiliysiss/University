package com.application.flatsandroid.network.models.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class LoginResult {
    @Expose
    @SerializedName("role")
    private Integer Role;

    public Integer getRole() {
        return Role;
    }

    public void setRole(Integer role) {
        Role = role;
    }
}
