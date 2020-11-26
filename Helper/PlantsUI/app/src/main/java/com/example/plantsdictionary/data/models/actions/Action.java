package com.example.plantsdictionary.data.models.actions;

import com.example.plantsdictionary.interfaces.models.IModelActionSerialize;
import com.fasterxml.jackson.annotation.JsonIgnore;

import java.util.List;

/**
 * Действие
 */
public class Action {

    /**
     * parcelableClass в Runtime класс через Class.for
     * Вычисляется 1 раз, кэш
     */
    @JsonIgnore
    private Class parcelableRuntimeClass;

    private String name;
    private String caption;
    /**
     * Куда переходить при onClick
     */
    private String navigateTo;
    private String image;
    /**
     * Аргументы перехода
     */
    private List<ActionArguments> actionArguments;
    /**
     * Класс модели при переходе
     */
    private String parcelableClass;
    /**
     * Id модели при переходе. Id - R.string.{name}
     */
    private String parcelableClassId;

    public String getParcelableClassId() {
        return parcelableClassId;
    }

    public void setParcelableClassId(String parcelableClassId) {
        this.parcelableClassId = parcelableClassId;
    }

    public String getParcelableClass() {
        return parcelableClass;
    }

    public void setParcelableClass(String parcelableClass) {
        this.parcelableClass = parcelableClass;
    }

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
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

    public String getNavigateTo() {
        return navigateTo;
    }

    public void setNavigateTo(String navigateTo) {
        this.navigateTo = navigateTo;
    }

    public List<ActionArguments> getActionArguments() {
        return actionArguments;
    }

    public void setActionArguments(List<ActionArguments> actionArguments) {
        this.actionArguments = actionArguments;
    }

    public Class<IModelActionSerialize> getParcelableRuntimeClass() throws ClassNotFoundException {
        if (parcelableClass == null)
            return null;
        if (parcelableRuntimeClass != null)
            return parcelableRuntimeClass;
        return parcelableRuntimeClass = Class.forName(parcelableClass);
    }
}
