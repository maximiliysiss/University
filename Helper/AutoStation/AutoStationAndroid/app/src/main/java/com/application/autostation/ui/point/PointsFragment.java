package com.application.autostation.ui.point;


import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.Point;
import com.application.autostation.ui.activities.AdminActivity;
import com.application.autostation.ui.fragments.ModelContainsFragment;
import com.application.autostation.ui.adapters.recyclerviews.RecyclerViewAdapter;
import com.application.autostation.ui.adapters.recyclerviews.ViewHolder.PointViewHolder;

/**
 * A simple {@link Fragment} subclass.
 */
public class PointsFragment extends ModelContainsFragment<AdminActivity> {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_points, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        Button add = getView().findViewById(R.id.add);
        add.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_point, getString(R.string.point_model), new Point()));

        RecyclerView recyclerView = getView().findViewById(R.id.points);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
        App.getPointRetrofit().getPoints().enqueue(new UniversalCallback<>(getContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_point, v -> new PointViewHolder(v)));
        }));
    }
}
