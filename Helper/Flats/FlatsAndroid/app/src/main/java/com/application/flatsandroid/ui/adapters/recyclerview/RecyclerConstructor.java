package com.application.flatsandroid.ui.adapters.recyclerview;

import android.view.View;

/**
 * Создать карточку
 * @param <T>
 */
public interface RecyclerConstructor<T> {
    T getView(View view);
}