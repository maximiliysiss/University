package com.school.android.ui.classes;


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
import com.school.android.models.network.input.Class;
import com.school.android.network.classes.CallbackAction;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.ViewHolders.ClassViewHolder;
import com.school.android.ui.fragments.ModelContainsFragment;

import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */
public class ClassFragment extends ModelContainsFragment<MainActivity> {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_class, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        Button button = getView().findViewById(R.id.add);
        button.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_class_element, getModelName(), new Class()));

        RecyclerView recyclerView = getView().findViewById(R.id.classes);
        recyclerView.setLayoutManager(new LinearLayoutManager(this.getContext()));
        App.getClassRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), object ->
                recyclerView.setAdapter(new RecyclerViewAdapter(object, R.layout.recycler_class, x -> new ClassViewHolder(x, getModelName())))));
    }

    @Override
    public String getModelName() {
        return getString(R.string.class_model);
    }
}
