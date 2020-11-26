package com.example.plantsdictionary.ui.controls.recyclerview.viewholder;

import android.view.View;

/**
 * Конструктор для карточек для RecylerView
 * @param <T>
 */
public interface CardViewConstructor<T> {
    T getCard(View view);
}
