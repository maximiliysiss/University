package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.content.Context;
import android.view.View;
import android.widget.ExpandableListView;
import android.widget.TextView;

import com.school.android.R;
import com.school.android.models.network.input.Mark;
import com.school.android.ui.adapters.expandablelist.ExpandableListAdapter;

import java.util.HashMap;
import java.util.List;

public class MarkScheduleExpandableConstructor extends AbstractExpandableConstructor<String, HashMap<String, List<Mark>>> {

    Context context;

    public MarkScheduleExpandableConstructor(HashMap<String, List<HashMap<String, List<Mark>>>> hashMap, Context context) {
        super(hashMap, R.layout.expandable_header, R.layout.expandable_expandable);
        this.context = context;
    }

    @Override
    public void constructGroup(View v, String elem, int index) {
        TextView name = v.findViewById(R.id.name);
        name.setText(elem);
    }

    @Override
    public void constructChild(View v, HashMap<String, List<Mark>> elem, int index, int childIndex) {
        ExpandableListView expandableListView = v.findViewById(R.id.expanable);
        expandableListView.setAdapter(new ExpandableListAdapter<>(context, new MarkScheduleInnerExpandableConstructor(elem, context)));
    }
}
