package com.example.plantsdictionary.ui.fragments.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.ui.controls.recyclerview.RecyclerCardViewAdapter;
import com.example.plantsdictionary.ui.controls.ui.ActionRecyclerViewHolder;

public class HomeFragment extends Fragment {

    private HomeViewModel homeViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        homeViewModel = new ViewModelProvider(this).get(HomeViewModel.class);
        View root = inflater.inflate(R.layout.fragment_home, container, false);

        final RecyclerView actionRecyclerView = root.findViewById(R.id.actions);
        GridLayoutManager gridLayoutManager = new GridLayoutManager(getContext(), 2, GridLayoutManager.VERTICAL, false);
        actionRecyclerView.setLayoutManager(gridLayoutManager);
        actionRecyclerView.setAdapter(new RecyclerCardViewAdapter(homeViewModel.getActionViewModels(), this, R.layout.action_item,
                view -> new ActionRecyclerViewHolder(view)));
        homeViewModel.getActionViewModels().observe(getViewLifecycleOwner(), actionViewModels -> actionRecyclerView.getAdapter().notifyDataSetChanged());
        return root;
    }
}