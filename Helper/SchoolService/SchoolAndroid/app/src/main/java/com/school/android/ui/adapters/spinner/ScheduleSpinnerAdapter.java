package com.school.android.ui.adapters.spinner;

import android.content.Context;

import com.school.android.models.network.input.Schedule;

import java.util.List;

public class ScheduleSpinnerAdapter extends SpinnerCustomModelAdapter<Schedule> {
    public ScheduleSpinnerAdapter(List<Schedule> data, int layout, Context context) {
        super(data, layout, context);
    }

    @Override
    public String getText(Schedule obj) {
        return obj.getLesson().getName();
    }
}
