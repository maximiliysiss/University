package com.example.storage.ui;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.storage.MainActivity;
import com.example.storage.R;

/**
 * Фрагмент с моделью
 * @param <T>
 */
public abstract class ObjectFragment<T> extends Fragment {

    /**
     * Объект
     */
    T obj;
    /**
     * Для изменения
     */
    boolean isEdit;

    public boolean isEdit() {
        return isEdit;
    }

    public void setEdit(boolean edit) {
        isEdit = edit;
    }

    public T getObj() {
        return obj;
    }

    public ObjectFragment<T> setObj(T obj) {
        this.obj = obj;
        return this;
    }
}
