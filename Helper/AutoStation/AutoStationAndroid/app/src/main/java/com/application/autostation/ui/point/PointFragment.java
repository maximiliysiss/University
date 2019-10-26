package com.application.autostation.ui.point;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;

import com.application.autostation.R;
import com.application.autostation.network.models.input.Point;
import com.application.autostation.ui.fragments.ModelFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class PointFragment extends ModelFragment<Point> {


    public PointFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_point, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
    }

    @Override
    public String getModelName() {
        return getString(R.string.point_model);
    }

    @Override
    public void loadObject() {

    }

    @Override
    public void add(Point obj) {

    }

    @Override
    public void delete(int id) {

    }

    @Override
    public void update(Point obj) {

    }
}
