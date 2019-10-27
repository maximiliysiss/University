package com.school.android.ui.adapters.spinner;

import android.content.Context;

import com.school.android.models.network.input.Teacher;

import java.util.List;

public class TeacherSpinnerAdapter extends SpinnerCustomModelAdapter<Teacher> {
    public TeacherSpinnerAdapter(List<Teacher> data, int layout, Context context) {
        super(data, layout, context);
    }

    @Override
    public String getText(Teacher obj) {
        return obj.getSecondName() + " " + obj.getName() + " " + obj.getSurname();
    }
}
