package com.example.plantsdictionary.ui.interfaces;

import android.os.Parcelable;

/**
 * Интерфейс навигации
 */
public interface ActivityNavigator {
    /**
     * Открыть фрагмент
     * @param layoutId
     */
    void navigateTo(int layoutId);

    /**
     * Открыть фрагмент с Bundle
     * @param layoutId
     * @param id
     * @param parcelable
     */
    void navigateTo(int layoutId, int id, Parcelable parcelable);
}
