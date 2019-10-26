package com.application.autostation.ui.point;


import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.application.autostation.R;

/**
 * A simple {@link Fragment} subclass.
 */
public class PointsFragment extends Fragment {


    public PointsFragment() {
        // Required empty public constructor
    }


    RecyclerView recyclerView;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_points, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
    }
}
