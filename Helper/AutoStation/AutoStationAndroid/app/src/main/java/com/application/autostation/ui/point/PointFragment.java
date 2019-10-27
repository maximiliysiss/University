package com.application.autostation.ui.point;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.Point;
import com.application.autostation.ui.activities.AdminActivity;
import com.application.autostation.ui.fragments.ModelFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class PointFragment extends ModelFragment<Point, AdminActivity> {


    EditText name;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_point, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        name = getView().findViewById(R.id.name);

        name.setText(getObj().getName());

        generateActions();
    }

    @Override
    public String getModelName() {
        return getString(R.string.point_model);
    }

    @Override
    public boolean loadObject() {
        String nameString = name.getText().toString().trim();

        if (nameString.length() == 0) {
            return false;
        }

        getObj().setName(nameString);
        return true;
    }

    @Override
    public void add(Point obj) {
        App.getPointRetrofit().create(obj).enqueue(new UniversalCallback<>(getContext(), x -> {
            getRealActivity().openFragment(R.id.navigation_points);
        }));
    }

    @Override
    public void delete(int id) {
        App.getPointRetrofit().delete(id).enqueue(new UniversalCallback<>(getContext(), x -> {
            getRealActivity().openFragment(R.id.navigation_points);
        }));
    }

    @Override
    public void update(Point obj) {
        App.getPointRetrofit().update(obj.getId(), obj).enqueue(new UniversalCallback<>(getContext(), x -> {
            getRealActivity().openFragment(R.id.navigation_points);
        }));
    }
}
