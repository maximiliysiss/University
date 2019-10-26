package com.application.autostation.ui.schedules;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.application.autostation.R;
import com.application.autostation.network.models.input.Schedule;
import com.application.autostation.ui.fragments.ModelFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class ScheduleFragment extends ModelFragment<Schedule> {


    public ScheduleFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_schedule, container, false);
    }

    @Override
    public String getModelName() {
        return getString(R.string.schedule_model);
    }

    @Override
    public void loadObject() {

    }

    @Override
    public void add(Schedule obj) {

    }

    @Override
    public void delete(int id) {

    }

    @Override
    public void update(Schedule obj) {

    }
}
