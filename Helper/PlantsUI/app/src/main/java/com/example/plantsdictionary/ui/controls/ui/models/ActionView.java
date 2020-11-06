package com.example.plantsdictionary.ui.controls.ui.models;

public class ActionView {
    private String name;
    private String caption;
    private int navigateTo;

    public ActionView(String name, String caption, int navigateTo) {
        this.name = name;
        this.caption = caption;
        this.navigateTo = navigateTo;
    }

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
