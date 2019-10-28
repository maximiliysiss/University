package com.school.android.ui.lessons;


import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Lesson;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.ViewHolders.LessonViewHolder;
import com.school.android.ui.fragments.ModelContainsFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class LessonsFragment extends ModelContainsFragment<MainActivity> {


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_lessons, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        Button add = getView().findViewById(R.id.add);
        add.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_lesson_element, getModelName(), new Lesson()));
        RecyclerView recyclerView = getView().findViewById(R.id.lessons);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        App.getLessonRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_lesson, v -> new LessonViewHolder(v, getModelName())));
        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.lesson_model);
    }
}
