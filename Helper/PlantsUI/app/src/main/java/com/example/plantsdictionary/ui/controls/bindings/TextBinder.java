package com.example.plantsdictionary.ui.controls.bindings;

import android.text.Editable;
import android.text.TextWatcher;

import androidx.lifecycle.MutableLiveData;

/**
 * Класс реакция на изменения текста с установкой данных в Observable класс
 */
public class TextBinder implements TextWatcher {
    MutableLiveData<String> stringLiveData;

    public TextBinder(MutableLiveData<String> stringLiveData) {
        this.stringLiveData = stringLiveData;
    }

    @Override
    public void beforeTextChanged(CharSequence s, int start, int count, int after) {
    }

    @Override
    public void onTextChanged(CharSequence s, int start, int before, int count) {
        stringLiveData.setValue(s.toString());
    }

    @Override
    public void afterTextChanged(Editable s) {

    }
}
