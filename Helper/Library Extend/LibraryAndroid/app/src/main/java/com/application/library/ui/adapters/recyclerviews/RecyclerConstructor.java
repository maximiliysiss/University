package com.application.library.ui.adapters.recyclerviews;

import android.view.View;

/**
 * Получить собранный элемент RecyclerView
 * @param <T>
 */
public interface RecyclerConstructor<T> {
    T getView(View view);
}