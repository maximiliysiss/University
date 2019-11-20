package com.application.carrepairandroid.network.callbacks;

/**
 * Обработка результата запроса
 * @param <T>
 */
public interface CallbackAction<T> {
    void process(T body);
}
