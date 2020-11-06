package com.example.plantsdictionary.ui.controls.ui.models;

import com.example.plantsdictionary.data.models.Action;

public class ActionViewModel {

    private Action action;

    public ActionViewModel(Action action) {
        this.action = action;
    }

    public String getCaption() {
        return action.getCaption();
    }
}
