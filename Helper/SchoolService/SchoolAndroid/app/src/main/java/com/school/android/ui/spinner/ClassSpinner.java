package com.school.android.ui.spinner;

import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.widget.Spinner;

import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.adapters.spinner.ClassSpinnerAdapter;
import com.school.android.utilities.CustomDate;
import com.school.android.utilities.DayUtils;

public class ClassSpinner extends SpinnerModelObserver<Class> {
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
        if (observable instanceof DayCalendar) {
            DayCalendar dayCalendar = (DayCalendar) observable;
            CustomDate customDate = dayCalendar.getCustomDate();
            App.getClassRetrofit().getClassByDay(customDate.getDayOfWeek()).enqueue(new UniversalCallback<>(getContext(), x -> {
                setAdapter(new ClassSpinnerAdapter(x, getContext()));
                trySetData();
                this.notifyObservers(true);
            }));
        }
    }
}
