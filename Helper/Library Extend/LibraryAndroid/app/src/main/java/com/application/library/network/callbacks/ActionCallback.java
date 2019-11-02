package com.application.library.network.callbacks;

/**
 * Определение для действия над телом зрезультат запроса
 * @param <T>
 */
public interface ActionCallback<T> {
    void process(T t);
}
