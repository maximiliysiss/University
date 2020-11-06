package com.example.plantsdictionary.data.models;

public class Action {
    private String name;
    private String caption;
    private int navigateTo;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getCaption() {
        return caption;
    }

    public void setCaption(String caption) {
        this.caption = caption;
    }

    public int getNavigateTo() {
        return navigateTo;
    }

    public void setNavigateTo(int navigateTo) {
        this.navigateTo = navigateTo;
    }
}
