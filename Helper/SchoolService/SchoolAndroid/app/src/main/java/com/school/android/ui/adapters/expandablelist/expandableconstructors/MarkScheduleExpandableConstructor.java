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

import org.w3c.dom.Text;

import java.util.HashMap;
import java.util.List;

public class MarkScheduleExpandableConstructor extends AbstractExpandableConstructor<String, List<Mark>> {


    private final MarkInnerConstructor markInnerConstructor;
    private final Context context;

    public MarkScheduleExpandableConstructor(HashMap<String, List<List<Mark>>> hashMap, Context context, MarkInnerConstructor markInnerConstructor) {
        super(hashMap, R.layout.expandable_header, R.layout.expandable_expandable);
        this.markInnerConstructor = markInnerConstructor;
        this.context = context;
    }

    @Override
    public void constructGroup(View v, String elem, int index) {
        TextView name = v.findViewById(R.id.name);
        name.setText(elem);
    }

    @Override
    public void constructChild(View v, List<Mark> elem, int index, int childIndex) {
        ExpandableListView expandableListView = v.findViewById(R.id.expanable);
        expandableListView.setAdapter(new ExpandableListAdapter<>(context, markInnerConstructor.constructor(,context)));
    }
}
