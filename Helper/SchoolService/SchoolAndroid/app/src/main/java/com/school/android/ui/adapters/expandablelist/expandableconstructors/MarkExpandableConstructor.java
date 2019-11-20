package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.os.Bundle;
import android.view.View;

import com.school.android.R;
import com.school.android.models.network.input.Mark;

import java.util.List;
import java.util.Map;

public class MarkExpandableConstructor extends SuperMarkExpandableConstructor {

    private final int classId;

    public MarkExpandableConstructor(Map<String, List<Mark>> hashMap, int classId) {
        super(hashMap);
        this.classId = classId;
    }


    @Override
    public void constructChild(View v, Mark elem, int index, int childIndex) {
        super.constructChild(v, elem, index, childIndex);
        v.setOnClickListener(v1 -> {
            Bundle bundle = new Bundle();
            bundle.putInt(getActivity(v1).getString(R.string.class_model), classId);
            bundle.putSerializable(getActivity(v1).getString(R.string.mark_model), elem);
            getRealActivity(v1).openFragment(R.id.navigation_mark_element, bundle);
        });
    }
}
