package com.school.android.ui.callbacks;

public interface FilterAction<T> {
    boolean filter(String filter, T model);
}
