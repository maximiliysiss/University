package com.school.android.ui.spinner;

import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;
import android.widget.CalendarView;
import android.widget.Spinner;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.school.android.utilities.CustomDate;
import com.school.android.utilities.DayUtils;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class DayCalendar extends CalendarView implements Observable, Observer {

    private CustomDate customDate;

    public CustomDate getCustomDate() {
        return customDate;
    }

    public void setCustomDate(CustomDate customDate) {
        this.customDate = customDate;
    }

    private void register() {
        this.setOnDateChangeListener((view, year, month, dayOfMonth) -> {
            customDate = new CustomDate(year, month + 1, dayOfMonth);
            notifyObservers();
        });
    }

    public DayCalendar(@NonNull Context context) {
        super(context);
        register();
    }

    public DayCalendar(@NonNull Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        register();
    }

    public DayCalendar(@NonNull Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
        register();
    }

    public DayCalendar(@NonNull Context context, @Nullable AttributeSet attrs, int defStyleAttr, int defStyleRes) {
        super(context, attrs, defStyleAttr, defStyleRes);
        register();
    }

    List<Observer> observerList = new ArrayList<>();

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
        for (Observer observer : observerList) {
            observer.notify(this);
        }
    }

    @Override
    public void notify(Observable observable) {
    }
}
