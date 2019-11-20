package com.application.carrepairandroid.ui.adapters.recyclerview.ViewHolders;

import android.view.View;

/**
 * Создание карточки
 * @param <T>
 */
public interface RecyclerViewConstructor<T> {
    T getView(View view);
}
