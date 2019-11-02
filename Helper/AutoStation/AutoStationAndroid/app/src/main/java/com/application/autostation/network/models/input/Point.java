package com.application.autostation.network.models.input;

import com.application.autostation.ui.models.Model;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Точка
 */
public class Point implements Model {
    @SerializedName("id")
    @Expose
    private Integer id = 0;
    @SerializedName("name")
    @Expose
    private String name;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
