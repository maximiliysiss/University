package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.content.Context;
import android.view.View;
import android.widget.TextView;

import com.school.android.R;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;
import com.school.android.models.network.input.Schedule;

import java.util.HashMap;
import java.util.List;

public class MarkScheduleInnerExpandableConstructor extends AbstractExpandableConstructor<Lesson, Mark> {

    Context context;

    public MarkScheduleInnerExpandableConstructor(HashMap<Lesson, List<Mark>> hashMap, Context context) {
        super(hashMap, R.layout.expandable_header, R.layout.expandable_content_mark);
        this.context = context;
    }

    @Override
    public void constructGroup(View v, Lesson elem, int index) {
        TextView name = v.findViewById(R.id.name);
        name.setText(elem.getName());
    }

    @Override
    public void constructChild(View v, Mark elem, int index, int childIndex) {

        TextView fio = v.findViewById(R.id.fio);
        TextView mark = v.findViewById(R.id.mark);
        fio.setText(elem.getChild().getFullName());
        mark.setText(elem.getMarkReal());
        v.setOnClickListener(v1 -> MarkScheduleInnerExpandableConstructor.this.onClick(elem));
    }

    protected void onClick(Mark mark) {

    }
}
