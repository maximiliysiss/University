package com.application.autostation.network.callbacks;

/**
 * Действие над результатом запроса
 * @param <T>
 */
public interface ActionCallback<T> {
    void process(T t);
}
