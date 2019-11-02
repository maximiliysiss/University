package com.school.android.ui.adapters.spinner;

import android.content.Context;

import com.school.android.R;
import com.school.android.models.network.input.Schedule;

import java.util.List;

public class ScheduleSpinnerAdapter extends SpinnerCustomModelAdapter<Schedule> {
    public ScheduleSpinnerAdapter(List<Schedule> data, Context context) {
        super(data, R.layout.spinner_item, context);
    }

    @Override
    public String getText(Schedule obj) {
        return obj.getLesson().getName();
    }
}
