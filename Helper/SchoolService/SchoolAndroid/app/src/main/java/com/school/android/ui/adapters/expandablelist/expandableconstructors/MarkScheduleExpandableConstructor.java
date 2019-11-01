package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.content.Context;
import android.graphics.Color;
import android.util.Pair;
import android.view.View;
import android.widget.ExpandableListView;
import android.widget.TextView;
import android.widget.Toast;

import com.school.android.R;
import com.school.android.models.extension.LessonWithMarks;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;
import com.school.android.ui.adapters.expandablelist.ExpandableListAdapter;
import com.school.android.utilities.DayUtils;

import org.w3c.dom.Text;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class MarkScheduleExpandableConstructor extends AbstractExpandableConstructor<Integer, List<Mark>> {


    private final MarkInnerConstructor markInnerConstructor;

    public MarkScheduleExpandableConstructor(Map<Integer, List<List<Mark>>> hashMap, MarkInnerConstructor markInnerConstructor) {
        super(hashMap, R.layout.expandable_header, R.layout.expandable_expandable);
        this.markInnerConstructor = markInnerConstructor;
    }

    @Override
    public void constructGroup(View v, Integer elem, int index) {
        TextView name = v.findViewById(R.id.name);
        name.setTextColor(Color.WHITE);
        name.setBackgroundColor(Color.BLACK);
        name.setText(DayUtils.getName(elem));
    }

    @Override
    public void constructChild(View v, List<Mark> elem, int index, int childIndex) {
        HashMap<Lesson, List<Mark>> lessonListHashMap = new HashMap<>();
        lessonListHashMap.put(elem.get(0).getSchedule().getLesson(), elem);
        ExpandableListView expandableListView = v.findViewById(R.id.expanable);
        expandableListView.setAdapter(new ExpandableListAdapter<>(v.getContext(), markInnerConstructor.constructor(lessonListHashMap, v.getContext())));
    }
}
