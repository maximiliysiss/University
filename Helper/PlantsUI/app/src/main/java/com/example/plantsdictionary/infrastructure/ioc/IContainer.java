package com.example.plantsdictionary.infrastructure.ioc;

public interface IContainer {
    <T> T resolve(Class<T> type);

    <I, T> void register(Class<I> i, T obj, ScopeType scopeType);
}
