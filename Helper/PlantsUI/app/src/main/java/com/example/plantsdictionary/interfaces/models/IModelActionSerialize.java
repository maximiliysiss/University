package com.example.plantsdictionary.interfaces.models;

import android.content.Context;
import android.os.Bundle;

import com.example.plantsdictionary.data.models.actions.ActionArguments;

import java.util.List;

/**
 * Интерфейс для модели, которая имеет расширенную сериализацию
 */
public interface IModelActionSerialize {
    /**
     * Сериализация по аргументам
     * @param argumentsList
     */
    void load(List<ActionArguments> argumentsList);

    /**
     * Сериализация по контекстую. Нужно для navigation.xml
     * @param context
     * @param bundle
     */
    void load(Context context, Bundle bundle);
}
