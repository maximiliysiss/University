package com.application.autostation.ui.schedules;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.Schedule;
import com.application.autostation.ui.activities.AdminActivity;
import com.application.autostation.ui.fragments.ModelContainsFragment;
import com.application.autostation.ui.adapters.recyclerviews.RecyclerViewAdapter;
import com.application.autostation.ui.adapters.recyclerviews.ViewHolder.ScheduleViewHolder;

/**
 * Вывод списка расписания
 */
public class SchedulesFragment extends ModelContainsFragment<AdminActivity> {


    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_schedules, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        Button add = getView().findViewById(R.id.add);
        add.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_schedule, getString(R.string.schedule_model), new Schedule()));

        RecyclerView recyclerView = getView().findViewById(R.id.schedules);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        App.getScheduleRetrofit().getSchedules().enqueue(new UniversalCallback<>(getContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_schedule, v -> new ScheduleViewHolder(v)));
        }));
    }
}