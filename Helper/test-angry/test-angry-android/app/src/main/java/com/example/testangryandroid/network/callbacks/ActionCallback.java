package com.example.testangryandroid.network.callbacks;

/**
 * Действие над результатом запроса
 * @param <T>
 */
public interface ActionCallback<T> {
    void process(T t);
}
