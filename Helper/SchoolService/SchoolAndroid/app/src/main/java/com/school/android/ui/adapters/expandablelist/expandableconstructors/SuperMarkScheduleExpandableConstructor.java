package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.content.Context;

import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;

import java.util.HashMap;
import java.util.List;

public class SuperMarkScheduleExpandableConstructor extends MarkScheduleInnerExpandableConstructor {
    public SuperMarkScheduleExpandableConstructor(HashMap<Lesson, List<Mark>> hashMap, Context context) {
        super(hashMap, context);
    }

    @Override
    protected void onClick(Mark mark) {
    }
}
