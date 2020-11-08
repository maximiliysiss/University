package com.example.plantsdictionary.ui.controls.ui.models;

import com.example.plantsdictionary.data.models.actions.Action;
import com.example.plantsdictionary.data.models.actions.ActionArguments;
import com.example.plantsdictionary.interfaces.models.IModelActionSerialize;

import java.util.ArrayList;
import java.util.List;

/**
 * Модель действия
 */
public class ActionViewModel {

    /**
     * Модель
     */
    private Action action;

    public ActionViewModel(Action action) {
        this.action = action;
    }

    public String getCaption() {
        return action.getCaption();
    }

    public String getNavigateTo() {
        return action.getNavigateTo();
    }

    public String getImage() {
        return action.getImage();
    }

    public List<ActionArguments> getActionArguments() {
        List<ActionArguments> actionArguments = action.getActionArguments();
        if (actionArguments != null)
            return actionArguments;
        return new ArrayList<>();
    }

    public Class<IModelActionSerialize> getParcelableClass() {
        try {
            return action.getParcelableRuntimeClass();
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
        return null;
    }

    public String getParcelableClassId() {
        return action.getParcelableClassId();
    }
}
