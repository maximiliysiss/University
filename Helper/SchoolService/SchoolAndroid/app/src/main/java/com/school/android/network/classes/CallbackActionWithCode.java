package com.school.android.network.classes;

public interface CallbackActionWithCode<T> {
    void process(int code, T object);
}
