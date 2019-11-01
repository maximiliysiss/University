package com.school.android.ui.spinner;

import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.widget.Spinner;

import com.school.android.utilities.DayUtils;

public class LessonSpinner extends SpinnerObserver {

    Spinner daySpinner;

    public void setDaySpinner(Spinner daySpinner) {
        this.daySpinner = daySpinner;
    }

    public LessonSpinner(Context context) {
        super(context);
    }

    public LessonSpinner(Context context, int mode) {
        super(context, mode);
    }

    public LessonSpinner(Context context, AttributeSet attrs) {
        super(context, attrs);
    }

    public LessonSpinner(Context context, AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }

    public LessonSpinner(Context context, AttributeSet attrs, int defStyleAttr, int mode) {
        super(context, attrs, defStyleAttr, mode);
    }

    public LessonSpinner(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode) {
        super(context, attrs, defStyleAttr, defStyleRes, mode);
    }

    public LessonSpinner(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode, Resources.Theme popupTheme) {
        super(context, attrs, defStyleAttr, defStyleRes, mode, popupTheme);
    }

    @Override
    public void notify(Observable observable) {
        if (observable instanceof ClassSpinner) {
            Class aClass = (Class) ((Spinner) observable).getSelectedItem();
            Integer day = DayUtils.getId(daySpinner.getSelectedItem().toString());


        }
    }
}
