package com.application.carrepairandroid.network.callbacks;

public interface CallbackAction<T> {
    void process(T body);
}
