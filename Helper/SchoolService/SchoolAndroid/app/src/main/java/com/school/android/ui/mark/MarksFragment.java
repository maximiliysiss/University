package com.school.android.ui.mark;


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
import com.school.android.ui.adapters.recyclerview.ViewHolders.TeacherMarkViewHolder;
import com.school.android.ui.fragments.ModelContainsFragment;

import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */
public class MarksFragment extends ModelContainsFragment<MainActivity> {


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_marks, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        RecyclerView recyclerView = getView().findViewById(R.id.marks);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        App.getClassRetrofit().getTeacherClasses(App.getUserContext().id).enqueue(new UniversalCallback<List<Class>>(getContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_class, v -> new TeacherMarkViewHolder(v, getModelName())));
        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.mark_model);
    }
}
