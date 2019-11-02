package com.application.library.ui.adapters.recyclerviews;

import android.view.View;

/**
 * Создать карточку
 * @param <T>
 */
public interface RecyclerConstructor<T> {
    T getView(View view);
}