package com.school.android.ui.schedule;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ExpandableListView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Schedule;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.expandablelist.ExpandableListAdapter;
import com.school.android.ui.adapters.expandablelist.expandableconstructors.ScheduleExpandableConstructor;
import com.school.android.ui.fragments.ModelContainsFragment;
import com.school.android.utilities.DayUtils;
import com.school.android.utilities.ScheduleUtilities;

import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static java.util.stream.Collectors.groupingBy;

public class ScheduleFragment extends ModelContainsFragment<MainActivity> {

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_schedule, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        Button add = getView().findViewById(R.id.add);
        add.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_schedule_element, getModelName(), new Schedule()));
        ExpandableListView expandableListView = getView().findViewById(R.id.schedules);
        App.getScheduleRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x ->
                expandableListView.setAdapter(new ExpandableListAdapter<>(getContext(), new ScheduleExpandableConstructor(ScheduleUtilities.sort(x))))));

        if (App.getUserType() == UserType.Student)
            add.setVisibility(View.INVISIBLE);
    }

    @Override
    public String getModelName() {
        return getString(R.string.schedule_model);
    }
}