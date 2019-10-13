package com.example.storage.ui.data.ViewHolder;

import android.view.View;

/**
 * Конструктор для карточек для RecylerView
 * @param <T>
 */
public interface CardViewConstructor<T> {
    T getCard(View view);
}
