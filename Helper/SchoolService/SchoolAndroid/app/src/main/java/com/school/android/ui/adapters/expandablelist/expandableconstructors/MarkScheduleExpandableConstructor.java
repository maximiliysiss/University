package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.content.Context;
import android.util.Pair;
import android.view.View;
import android.widget.ExpandableListView;
import android.widget.TextView;

import com.school.android.R;
import com.school.android.models.extension.LessonWithMarks;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;
import com.school.android.ui.adapters.expandablelist.ExpandableListAdapter;

import java.util.HashMap;
import java.util.List;

public class MarkScheduleExpandableConstructor extends AbstractExpandableConstructor<String, List<LessonWithMarks>> {

    public MarkScheduleExpandableConstructor(HashMap<String, List<List<LessonWithMarks>>> hashMap) {
        super(hashMap, R.layout.expandable_header, R.layout.expandable_expandable);
    }

    @Override
    public void constructGroup(View v, String elem, int index) {

    }

    @Override
    public void constructChild(View v, List<LessonWithMarks> elem, int index, int childIndex) {

    }
}
