package com.school.android.ui.archive;


import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.ViewHolders.ArchiveViewHolder;
import com.school.android.ui.fragments.ModelContainsFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class ArchiveFragment extends ModelContainsFragment<MainActivity> {


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_archive, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        RecyclerView recyclerView = getView().findViewById(R.id.archived);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        App.getChildrenRetrofit().archived().enqueue(new UniversalCallback<>(getContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_children, v -> new ArchiveViewHolder(v, getModelName())));
        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }
}
