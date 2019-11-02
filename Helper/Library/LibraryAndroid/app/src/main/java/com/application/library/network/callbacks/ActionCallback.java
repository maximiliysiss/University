package com.application.library.network.callbacks;

/**
 * Действие над телом результата запроса
 * @param <T>
 */
public interface ActionCallback<T> {
    void process(T t);
}
