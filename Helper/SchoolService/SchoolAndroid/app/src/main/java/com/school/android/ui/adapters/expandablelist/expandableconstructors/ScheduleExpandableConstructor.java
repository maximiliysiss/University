package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.view.View;
import android.widget.TextView;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Schedule;
import com.school.android.utilities.LessonUtils;

import java.util.HashMap;
import java.util.List;

public class ScheduleExpandableConstructor extends AbstractExpandableConstructor<String, Schedule> {

    public ScheduleExpandableConstructor(HashMap<String, List<Schedule>> hashMap) {
        super(hashMap, R.layout.expandable_header, R.layout.expandable_content_schedule);
    }

    @Override
    public void constructGroup(View v, String elem, int index) {
        TextView name = v.findViewById(R.id.name);
        name.setText(elem);
    }

    @Override
    public void constructChild(View v, Schedule elem, int index, int childIndex) {
        TextView className = v.findViewById(R.id.class_name);
        TextView lesson = v.findViewById(R.id.lesson);
        TextView time = v.findViewById(R.id.time);

        className.setText(elem.get_class().getName());
        lesson.setText(elem.getLesson());
        time.setText(LessonUtils.getStrings(elem.getLessonNumber()));
        if (App.getUserType() != UserType.Student)
            v.setOnClickListener(v1 -> getRealActivity(v1).openFragment(R.id.navigation_schedule_element, getActivity(v1).getString(R.string.schedule_model), elem));
    }
}
