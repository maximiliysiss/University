package com.application.autostation.ui.adapters.recyclerviews;

import android.view.View;

/**
 * Конструктор карточки
 * @param <T>
 */
public interface RecyclerConstructor<T> {
    T getView(View view);
}