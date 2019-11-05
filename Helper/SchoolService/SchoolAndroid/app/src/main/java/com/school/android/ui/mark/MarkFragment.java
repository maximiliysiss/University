package com.school.android.ui.mark;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CalendarView;
import android.widget.ExpandableListView;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Mark;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.expandablelist.ExpandableListAdapter;
import com.school.android.ui.adapters.expandablelist.expandableconstructors.MarkExpandableConstructor;
import com.school.android.ui.fragments.ModelContainsFragment;
import com.school.android.utilities.CustomDate;

import java.util.Calendar;
import java.util.stream.Collectors;

public class MarkFragment extends ModelContainsFragment<MainActivity> {

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_mark, container, false);
    }

    ExpandableListView expandableListView;
    int classId;

    @Override
    public void onStart() {
        super.onStart();

        CalendarView calendarView = getView().findViewById(R.id.calendar);
        Button add = getView().findViewById(R.id.add);
        classId = getArguments().getInt(getString(R.string.mark_model), -1);
        add.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_mark_element, getModelName(), new Mark()));
        expandableListView = getView().findViewById(R.id.marks);
        calendarView.setOnDateChangeListener((view, year, month, dayOfMonth) -> loadData(year, month, dayOfMonth));

        calendarView.setDate(Calendar.getInstance().getTime().getTime());

        CustomDate customDate = new CustomDate(calendarView.getDate());
        loadData(customDate.getYear(), customDate.getMonth() - 1, customDate.getDay());

    }

    public void loadData(int year, int month, int dayOfMonth) {
        App.getMarkRetrofit().getMarks(classId, year, month + 1, dayOfMonth).enqueue(new UniversalCallback<>(getContext(), x -> {
            expandableListView.setAdapter(new ExpandableListAdapter<>(getContext(), new MarkExpandableConstructor(
                    x.stream().collect(Collectors.groupingBy(z -> z.getSchedule().getLesson().getId())).entrySet().stream()
                            .collect(Collectors.toMap(z -> z.getValue().get(0).getSchedule().getLesson().getName(), z -> z.getValue()))
            )));
        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.mark_model);
    }
}