package com.school.android.ui.superclass;


import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.ViewHolders.ClassViewHolder;
import com.school.android.ui.adapters.recyclerview.ViewHolders.TeacherClassViewHolder;
import com.school.android.ui.fragments.ModelContainsFragment;

import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */
public class SuperClassFragment extends ModelContainsFragment<MainActivity> {


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_super_class, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        RecyclerView classes = getView().findViewById(R.id.classes);
        classes.setLayoutManager(new LinearLayoutManager(getContext()));
        App.getClassRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            classes.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_class, v -> new TeacherClassViewHolder(v, getModelName())));
        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.class_model);
    }
}
