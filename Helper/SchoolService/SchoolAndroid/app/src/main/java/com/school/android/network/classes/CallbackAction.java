package com.school.android.network.classes;

public interface CallbackAction<T> {
    void process(T object);
}
