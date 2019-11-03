package com.school.android.ui.spinner;

import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.widget.Spinner;

import com.school.android.application.App;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.Class;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.adapters.spinner.ChildrenSpinnerAdapter;

import java.util.ArrayList;
import java.util.List;

public class StudentSpinner extends SpinnerObserver {
    public StudentSpinner(Context context) {
        super(context);
    }

    public StudentSpinner(Context context, int mode) {
        super(context, mode);
    }

    public StudentSpinner(Context context, AttributeSet attrs) {
        super(context, attrs);
    }

    public StudentSpinner(Context context, AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }

    public StudentSpinner(Context context, AttributeSet attrs, int defStyleAttr, int mode) {
        super(context, attrs, defStyleAttr, mode);
    }

    public StudentSpinner(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode) {
        super(context, attrs, defStyleAttr, defStyleRes, mode);
    }

    public StudentSpinner(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode, Resources.Theme popupTheme) {
        super(context, attrs, defStyleAttr, defStyleRes, mode, popupTheme);
    }

    @Override
    public void notify(Observable observable) {
        if (observable instanceof ClassSpinner) {
            Class aClass = (Class) ((Spinner) observable).getSelectedItem();

            if (aClass == null) {
                setAdapter(new ChildrenSpinnerAdapter(new ArrayList<>(), getContext()));
                this.notifyObservers(true);
                return;
            }

            App.getChildrenRetrofit().getChildrenByClass(aClass.getId()).enqueue(new UniversalCallback<>(getContext(), x -> {
                setAdapter(new ChildrenSpinnerAdapter(x, getContext()));
                this.notifyObservers(true);
            }));
        }
    }
}
