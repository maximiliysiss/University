package com.application.autostation.ui.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.ui.adapters.recyclerviews.RecyclerViewAdapter;
import com.application.autostation.ui.adapters.recyclerviews.ViewHolder.ScheduleBuyViewHolder;

public class UserActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_user);
    }

    @Override
    protected void onStart() {
        super.onStart();

        RecyclerView recyclerView = findViewById(R.id.schedules);
        recyclerView.setLayoutManager(new LinearLayoutManager(getBaseContext()));
        App.getScheduleRetrofit().getSchedules().enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_schedule, v -> new ScheduleBuyViewHolder(v)));
        }));
    }
}
