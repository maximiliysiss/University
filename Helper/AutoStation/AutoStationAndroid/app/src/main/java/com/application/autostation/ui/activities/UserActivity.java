package com.application.autostation.ui.activities;

import android.content.Intent;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.Schedule;
import com.application.autostation.ui.adapters.recyclerviews.RecyclerViewAdapter;
import com.application.autostation.ui.adapters.recyclerviews.ViewHolder.ScheduleBuyViewHolder;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.TreeMap;

import static java.util.stream.Collectors.groupingBy;


/**
 * Форма покупки билетов
 */
public class UserActivity extends AppCompatActivity {

    /**
     * Создание формы
     *
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_user);
    }

    /**
     * При нажатии назад вернемся на форму логина
     */
    @Override
    public void onBackPressed() {
        startActivity(new Intent(this, LoginActivity.class));
    }

    /**
     * Старт формы
     */
    @Override
    protected void onStart() {
        super.onStart();

        RecyclerView recyclerView = findViewById(R.id.schedules);
        recyclerView.setLayoutManager(new LinearLayoutManager(getBaseContext()));
        /**
         * загрузим расписание
         */
        App.getScheduleRetrofit().getSchedules().enqueue(new UniversalCallback<>(getBaseContext(), x -> {

            /**
             * Отсортируем по дню и времени
             */
            HashMap<Integer, List<Schedule>> hashMap = (HashMap<Integer, List<Schedule>>) x.stream().collect(groupingBy(Schedule::getDayOfWeek));
            hashMap.forEach((k, l) -> {
                l.sort((o1, o2) -> o1.getTime().compareTo(o2.getTime()));
            });
            TreeMap<Integer, List<Schedule>> treeMap = new TreeMap<>(hashMap);

            ArrayList<Schedule> schedules = new ArrayList<>();
            treeMap.forEach((k, v) -> {
                schedules.addAll(v);
            });

            recyclerView.setAdapter(new RecyclerViewAdapter(schedules, R.layout.recycler_schedule, v -> new ScheduleBuyViewHolder(v)));
        }));
    }
}
