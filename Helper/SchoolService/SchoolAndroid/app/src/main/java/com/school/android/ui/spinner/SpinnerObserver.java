package com.school.android.ui.spinner;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.widget.Spinner;

import androidx.annotation.Nullable;

import java.util.ArrayList;
import java.util.List;

@SuppressLint("AppCompatCustomView")
public abstract class SpinnerObserver extends Spinner implements Observable, Observer {

    List<Observer> observerList = new ArrayList<>();

    public SpinnerObserver(Context context) {
        super(context);
    }

    public SpinnerObserver(Context context, int mode) {
        super(context, mode);
    }

    public SpinnerObserver(Context context, AttributeSet attrs) {
        super(context, attrs);
    }

    public SpinnerObserver(Context context, AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }

    public SpinnerObserver(Context context, AttributeSet attrs, int defStyleAttr, int mode) {
        super(context, attrs, defStyleAttr, mode);
    }

    public SpinnerObserver(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode) {
        super(context, attrs, defStyleAttr, defStyleRes, mode);
    }

    public SpinnerObserver(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode, Resources.Theme popupTheme) {
        super(context, attrs, defStyleAttr, defStyleRes, mode, popupTheme);
    }

    @Override
    public void AddObserver(Observer observer) {
        observerList.add(observer);
    }

    @Override
    public void removeObserver(Observer observer) {
        observerList.remove(observer);
    }

    @Override
    public void notifyObservers() {
        observerList.forEach(x -> x.notify(this));
    }

    @Override
    public void setOnItemSelectedListener(@Nullable OnItemSelectedListener listener) {
        super.setOnItemSelectedListener(listener);
        notifyObservers();
    }
}
