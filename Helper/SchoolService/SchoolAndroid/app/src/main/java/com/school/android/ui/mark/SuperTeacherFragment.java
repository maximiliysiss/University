package com.school.android.ui.mark;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ExpandableListView;

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

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
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
        if (classId > 0) {
            App.getClassRetrofit().getClassMarks(classId).enqueue(new UniversalCallback<>(getContext(), x -> {
                Map<String, List<Mark>> byDays = x.stream().collect(groupingBy(m -> DayUtils.getName(m.getSchedule().getDayOfWeek())));
                Map<String, List<List<Mark>>> data = new HashMap<>();

                byDays.forEach((k, v) -> {

                    List<List<Mark>> rec = new ArrayList<>();
                    Map<Lesson, List<Mark>> groupByLesson = v.stream().collect(groupingBy(m -> m.getSchedule().getLesson()));
                    groupByLesson.forEach((ki, li) -> {
                        rec.add(li);
                    });
                    data.put(k, rec);
                });

                expandableListView.setAdapter(new ExpandableListAdapter<>(getContext(),
                        new MarkScheduleExpandableConstructor((HashMap<String, List<List<Mark>>>) data, getContext(),
                                (d, c) -> new SuperMarkScheduleExpandableConstructor(d, c))));
            }));
        }
    }

    @Override
    public String getModelName() {
        return getString(R.string.mark_model);
    }
}
