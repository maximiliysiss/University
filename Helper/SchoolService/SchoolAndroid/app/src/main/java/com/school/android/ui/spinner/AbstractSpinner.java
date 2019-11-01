package com.school.android.ui.spinner;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.widget.Spinner;

import java.util.ArrayList;
import java.util.List;

@SuppressLint("AppCompatCustomView")
public class AbstractSpinner extends Spinner implements Observable {
    List<Observer> observerList = new ArrayList<>();

    public AbstractSpinner(Context context) {
        super(context);
    }

    public AbstractSpinner(Context context, int mode) {
        super(context, mode);
    }

    public AbstractSpinner(Context context, AttributeSet attrs) {
        super(context, attrs);
    }

    public AbstractSpinner(Context context, AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }

    public AbstractSpinner(Context context, AttributeSet attrs, int defStyleAttr, int mode) {
        super(context, attrs, defStyleAttr, mode);
    }

    public AbstractSpinner(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode) {
        super(context, attrs, defStyleAttr, defStyleRes, mode);
    }

    public AbstractSpinner(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode, Resources.Theme popupTheme) {
        super(context, attrs, defStyleAttr, defStyleRes, mode, popupTheme);
    }

    @Override
    public void AddObserver(Observer observer) {
        observerList.add(observer);
    }

    @Override
    public void RemoveObserver(Observer observer) {
        observerList.remove(observer);
    }

    @Override
    public void notifyObservers() {
        observerList.forEach(x -> x.notify(this));
    }
}
