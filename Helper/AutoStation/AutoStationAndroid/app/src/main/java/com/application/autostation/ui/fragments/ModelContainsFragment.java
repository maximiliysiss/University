package com.application.autostation.ui.fragments;

import android.app.Activity;

import androidx.fragment.app.Fragment;

/**
 * Фрагмент, который содержит модели
 * @param <A> базовая форма
 */
public class ModelContainsFragment<A extends Activity> extends Fragment {

    /**
     * Получить форму
     * @return
     */
    public A getRealActivity() {
        return (A) getActivity();
    }

}
