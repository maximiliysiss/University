package com.school.android.ui.workers;


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
import com.school.android.models.network.input.User;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.ViewHolders.UserViewHolder;
import com.school.android.ui.adapters.recyclerview.ViewHolders.WorkerViewHolder;
import com.school.android.ui.fragments.ModelContainsFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class WorkerFragment extends ModelContainsFragment<MainActivity> {


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_worker, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        Button add = getView().findViewById(R.id.add);
        add.setOnClickListener(v -> {
            Bundle bundle = new Bundle();
            bundle.putBoolean(getString(R.string.is_change), true);
            bundle.putInt(getString(R.string.back), R.id.navigation_workers);
            getRealActivity().openFragment(R.id.navigation_users_element, getModelName(), new User(), bundle);
        });

        RecyclerView recyclerView = getView().findViewById(R.id.users);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        App.getUserRetrofit().getWorkers().enqueue(new UniversalCallback<>(getContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_user, y -> new WorkerViewHolder(y, getModelName())));
        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }
}
