package com.school.android.ui.mark;


import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CalendarView;
import android.widget.ExpandableListView;

import androidx.fragment.app.Fragment;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Mark;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.expandablelist.ExpandableListAdapter;
import com.school.android.ui.adapters.expandablelist.expandableconstructors.SuperMarkExpandableConstructor;
import com.school.android.ui.fragments.ModelContainsFragment;
import com.school.android.utilities.CustomDate;

import java.util.Calendar;
import java.util.List;
import java.util.Map;

import static java.util.stream.Collectors.groupingBy;
import static java.util.stream.Collectors.toMap;

/**
 * A simple {@link Fragment} subclass.
 */
public class SuperTeacherFragment extends ModelContainsFragment<MainActivity> {

    ExpandableListView expandableListView;
    int classId;
    CalendarView calendarView;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_super_teacher, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        calendarView = getView().findViewById(R.id.calendar);
        classId = getArguments().getInt(getString(R.string.mark_model), 0);
        expandableListView = getView().findViewById(R.id.marks);
        calendarView.setOnDateChangeListener((view, year, month, dayOfMonth) -> {
            loadList(year, month, dayOfMonth);
        });

        CustomDate customDate = new CustomDate(calendarView.getDate());
        loadList(customDate.getYear(), customDate.getMonth() - 1, customDate.getDay());

        calendarView.setDate(Calendar.getInstance().getTime().getTime());
    }

    private void loadList(int year, int month, int dayOfMonth) {
        App.getMarkRetrofit().getMyClass(classId, year, month + 1, dayOfMonth)
                .enqueue(new UniversalCallback<>(getContext(), x -> {
                    Map<String, List<Mark>> map = x.stream().collect(groupingBy(z -> z.getScheduleId())).entrySet().stream()
                            .collect(toMap(z -> z.getValue().get(0).getSchedule().getLesson().getName(), z -> z.getValue()));
                    expandableListView.setAdapter(new ExpandableListAdapter<>(getContext(), new SuperMarkExpandableConstructor(map)));
                }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.mark_model);
    }
}
