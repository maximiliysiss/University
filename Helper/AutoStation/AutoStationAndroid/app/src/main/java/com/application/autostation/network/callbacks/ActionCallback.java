package com.application.autostation.network.callbacks;

public interface ActionCallback<T> {
    void process(T t);
}
