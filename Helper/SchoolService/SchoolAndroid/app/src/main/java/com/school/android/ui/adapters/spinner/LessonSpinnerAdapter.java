package com.school.android.ui.adapters.spinner;

import android.content.Context;

import com.school.android.models.network.input.Lesson;

import java.util.List;

public class LessonSpinnerAdapter extends SpinnerCustomModelAdapter<Lesson> {
    public LessonSpinnerAdapter(List<Lesson> data, int layout, Context context) {
        super(data, layout, context);
    }

    @Override
    public String getText(Lesson obj) {
        return obj.getName();
    }
}
