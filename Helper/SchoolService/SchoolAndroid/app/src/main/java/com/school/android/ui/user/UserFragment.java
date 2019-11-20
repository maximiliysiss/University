package com.school.android.ui.user;


import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.Toast;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.User;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapterConstructor;
import com.school.android.ui.adapters.recyclerview.ViewHolders.UserViewHolder;
import com.school.android.ui.callbacks.FilterAction;
import com.school.android.ui.fragments.BaseFragment;
import com.school.android.ui.fragments.ModelContainsFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserFragment extends ModelContainsFragment<MainActivity> {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_user, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
        Button button = getView().findViewById(R.id.add);
        button.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_users_element, getModelName(), new User()));

        RecyclerView recyclerView = getView().findViewById(R.id.users);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        setOnSearchReaction(recyclerView, App.getUserRetrofit().getModels(), (filter, model) -> model.getFullName().toLowerCase().contains(filter),
                new RecyclerViewAdapterConstructor<>(R.layout.recycler_user, y -> new UserViewHolder(y, getModelName())));

        App.getUserRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_user, y -> new UserViewHolder(y, getModelName())));
        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }
}
