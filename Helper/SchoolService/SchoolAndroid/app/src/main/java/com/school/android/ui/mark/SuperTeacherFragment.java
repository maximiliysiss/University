package com.school.android.ui.mark;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ExpandableListView;
import android.widget.Toast;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.extension.LessonWithMarks;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.expandablelist.ExpandableListAdapter;
import com.school.android.ui.adapters.expandablelist.expandableconstructors.MarkScheduleExpandableConstructor;
import com.school.android.ui.adapters.expandablelist.expandableconstructors.SuperMarkScheduleExpandableConstructor;
import com.school.android.ui.fragments.ModelContainsFragment;
import com.school.android.utilities.DayUtils;
import com.school.android.utilities.MarkUtilities;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.SortedSet;
import java.util.TreeSet;
import java.util.stream.Collectors;

import static java.util.stream.Collectors.groupingBy;

/**
 * A simple {@link Fragment} subclass.
 */
public class SuperTeacherFragment extends ModelContainsFragment<MainActivity> {

    ExpandableListView expandableListView;
    int classId;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_super_teacher, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        classId = getArguments().getInt(getString(R.string.mark_model), 0);
        expandableListView = getView().findViewById(R.id.marks);
        expandableListView.setOnChildClickListener(new ExpandableListView.OnChildClickListener() {
            @Override
            public boolean onChildClick(ExpandableListView parent, View v, int groupPosition, int childPosition, long id) {
                Toast.makeText(v.getContext(), "Test " + childPosition, Toast.LENGTH_SHORT).show();
                return false;
            }
        });
        if (classId > 0) {
            App.getClassRetrofit().getClassMarks(classId).enqueue(new UniversalCallback<>(getContext(), x -> {
                expandableListView.setAdapter(new ExpandableListAdapter<>(getContext(),
                        new MarkScheduleExpandableConstructor(MarkUtilities.getData(x), (d, c) -> new SuperMarkScheduleExpandableConstructor(d, c))));
            }));
        }
    }

    @Override
    public String getModelName() {
        return getString(R.string.mark_model);
    }
}
