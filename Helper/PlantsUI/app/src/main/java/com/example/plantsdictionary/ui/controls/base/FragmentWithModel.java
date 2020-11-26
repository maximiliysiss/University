package com.example.plantsdictionary.ui.controls.base;

import android.os.Bundle;
import android.os.Parcelable;

import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

/**
 * Фрагмент с моделью
 * @param <T>
 */
public class FragmentWithModel<T extends Parcelable> extends Fragment {

    /**
     * Название модели
     */
    private int modelId;
    /**
     * Кэш модели
     */
    protected T model;

    public FragmentWithModel(int modelId) {
        this.modelId = modelId;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Bundle bundle = getArguments();
        // Вытащим модель
        if (bundle != null && bundle.containsKey(getString(modelId))) {
            model = bundle.getParcelable(getString(modelId));
        }

    }
}
