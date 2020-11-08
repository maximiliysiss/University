package com.example.plantsdictionary.ui.controls.base;

import android.os.Bundle;
import android.os.Parcelable;

import androidx.annotation.Nullable;

import com.example.plantsdictionary.interfaces.models.IModelActionSerialize;

/**
 * Фрагмент с расширенной моделью IModelActionSerialize
 *
 * @param <T>
 */
public class FragmentWithBundleLoadModel<T extends IModelActionSerialize & Parcelable> extends FragmentWithModel<T> {
    /**
     * Класс модели
     */
    private final Class<T> klass;

    public FragmentWithBundleLoadModel(int modelId, Class<T> klass) {
        super(modelId);
        this.klass = klass;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // Если есть аргументы, но базовая реализация не смогла выставить модель, то попробуем сделать это из контекста
        if (getArguments() != null && model == null) {
            try {
                model = klass.newInstance();
                model.load(getContext(), getArguments());
            } catch (IllegalAccessException | java.lang.InstantiationException e) {
                e.printStackTrace();
            }
        }
    }
}
