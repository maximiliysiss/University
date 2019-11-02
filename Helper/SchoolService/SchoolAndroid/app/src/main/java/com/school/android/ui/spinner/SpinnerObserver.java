package com.school.android.ui.spinner;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Spinner;

import androidx.annotation.Nullable;

import java.util.ArrayList;
import java.util.List;

@SuppressLint("AppCompatCustomView")
public abstract class SpinnerObserver extends Spinner implements Observable, Observer {

    List<Observer> observerList = new ArrayList<>();

    private void registerLogic() {
        this.setOnItemSelectedListener(new OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                SpinnerObserver.this.notifyObservers();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }

    public SpinnerObserver(Context context) {
        super(context);
        registerLogic();
    }

    public SpinnerObserver(Context context, int mode) {
        super(context, mode);
        registerLogic();
    }

    public SpinnerObserver(Context context, AttributeSet attrs) {
        super(context, attrs);
        registerLogic();
    }

    public SpinnerObserver(Context context, AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
        registerLogic();
    }

    public SpinnerObserver(Context context, AttributeSet attrs, int defStyleAttr, int mode) {
        super(context, attrs, defStyleAttr, mode);
        registerLogic();
    }

    public SpinnerObserver(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode) {
        super(context, attrs, defStyleAttr, defStyleRes, mode);
        registerLogic();
    }

    public SpinnerObserver(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode, Resources.Theme popupTheme) {
        super(context, attrs, defStyleAttr, defStyleRes, mode, popupTheme);
        registerLogic();
    }

    @Override
    public void addObserver(Observer observer) {
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
