package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.view.View;
import android.widget.TextView;

import com.school.android.R;
import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class SuperMarkExpandableConstructor extends AbstractExpandableConstructor<String, Mark> {
    public SuperMarkExpandableConstructor(Map<String, List<Mark>> hashMap) {
        super(hashMap, R.layout.expandable_header, R.layout.expandable_content_mark);
    }

    @Override
    public void constructGroup(View v, String elem, int index) {
        TextView name = v.findViewById(R.id.name);
        name.setText(elem);
    }

    @Override
    public void constructChild(View v, Mark elem, int index, int childIndex) {
        TextView fio = v.findViewById(R.id.fio);
        TextView mark = v.findViewById(R.id.mark);

        fio.setText(elem.getChild().getFullName());
        mark.setText(String.valueOf(elem.getMarkReal()));
    }
}
