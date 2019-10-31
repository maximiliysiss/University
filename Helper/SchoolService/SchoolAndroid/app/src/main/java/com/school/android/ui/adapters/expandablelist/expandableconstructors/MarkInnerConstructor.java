package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.content.Context;

import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;

import java.util.HashMap;
import java.util.List;

public interface MarkInnerConstructor {
    MarkScheduleInnerExpandableConstructor constructor(HashMap<Lesson, List<Mark>> hashMap, Context context);
}
