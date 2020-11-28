package com.example.plantsdictionary.infrastructure.threads;

/**
 * Интерфейс для анонимного класса / делегата
 * @param <T>
 */
public interface ThreadResult<T> {
    T get();
}
