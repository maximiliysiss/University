package com.example.plantsdictionary.infrastructure.ioc;

/**
 * Самописный контейнер. Дальше мигрировать на существующий. Заглушка для слоя контейнера
 */
public interface IContainer {
    /**
     * Получить сущность
     * @param type
     * @param <T>
     * @return
     */
    <T> T resolve(Class<T> type);

    /**
     * Зарегистрировать класс
     * @param i
     * @param obj
     * @param scopeType
     * @param <I>
     * @param <T>
     */
    <I, T> void register(Class<I> i, T obj, ScopeType scopeType);
}
