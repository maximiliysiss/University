package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.view.View;

import com.school.android.models.network.input.Mark;
import com.school.android.ui.mark.SuperTeacherFragment;

import java.util.List;
import java.util.Map;

public class MarkExpandableConstructor extends SuperMarkExpandableConstructor {

    public MarkExpandableConstructor(Map<String, List<Mark>> hashMap) {
        super(hashMap);
    }


    @Override
    public void constructChild(View v, Mark elem, int index, int childIndex) {
        super.constructChild(v, elem, index, childIndex);

        v.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                
            }
        });
    }
}
