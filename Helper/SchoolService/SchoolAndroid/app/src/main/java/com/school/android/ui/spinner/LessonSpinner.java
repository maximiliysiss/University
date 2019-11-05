package com.school.android.ui.spinner;

import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.widget.CalendarView;
import android.widget.Spinner;

import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Schedule;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.adapters.spinner.ScheduleSpinnerAdapter;
import com.school.android.utilities.CustomDate;
import com.school.android.utilities.DayUtils;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

public class LessonSpinner extends SpinnerModelObserver<Schedule> {

    DayCalendar daySpinner;

    public void setDaySpinner(DayCalendar daySpinner) {
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
            CustomDate customDate = daySpinner.getCustomDate();

            if (aClass == null) {
                setAdapter(new ScheduleSpinnerAdapter(new ArrayList<>(), getContext()));
                this.notifyObservers(true);
                return;
            }

            App.getScheduleRetrofit().getScheduleByClassAndDay(customDate.getDayOfWeek(), aClass.getId()).enqueue(new UniversalCallback<>(getContext(), x -> {
                setAdapter(new ScheduleSpinnerAdapter(x, getContext()));
                trySetData();
                this.notifyObservers(true);
            }));
        }
    }
}
