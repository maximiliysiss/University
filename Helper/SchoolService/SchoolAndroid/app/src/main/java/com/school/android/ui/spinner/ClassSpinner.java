package com.school.android.ui.spinner;

import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.widget.Spinner;

import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.adapters.spinner.ClassSpinnerAdapter;
import com.school.android.utilities.DayUtils;

import java.util.List;

public class ClassSpinner extends SpinnerObserver {
    public ClassSpinner(Context context) {
        super(context);
    }

    public ClassSpinner(Context context, int mode) {
        super(context, mode);
    }

    public ClassSpinner(Context context, AttributeSet attrs) {
        super(context, attrs);
    }

    public ClassSpinner(Context context, AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }

    public ClassSpinner(Context context, AttributeSet attrs, int defStyleAttr, int mode) {
        super(context, attrs, defStyleAttr, mode);
    }

    public ClassSpinner(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode) {
        super(context, attrs, defStyleAttr, defStyleRes, mode);
    }

    public ClassSpinner(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode, Resources.Theme popupTheme) {
        super(context, attrs, defStyleAttr, defStyleRes, mode, popupTheme);
    }

    @Override
    public void notify(Observable observable) {
        if (observable instanceof DaySpinner) {
            Integer dayInteger = DayUtils.getId(((Spinner) observable).getSelectedItem().toString());
            App.getClassRetrofit().getClassByDay(dayInteger).enqueue(new UniversalCallback<>(getContext(), x -> {
                setAdapter(new ClassSpinnerAdapter(x, getContext()));
            }));
        }
    }
}
