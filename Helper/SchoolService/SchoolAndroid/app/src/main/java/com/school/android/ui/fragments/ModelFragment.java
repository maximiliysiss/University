package com.school.android.ui.fragments;

import android.app.Activity;
import android.view.View;
import android.widget.Button;

import com.school.android.R;
import com.school.android.models.network.FragmentModel;
import com.school.android.ui.activity.ActivityFragmenter;

import java.io.Serializable;

public abstract class ModelFragment<T extends ActivityFragmenter, M extends FragmentModel> extends ModelContainsFragment<T> {

    M model;
    boolean isEdit;

    protected int backLayout;

    public ModelFragment(int backLayout) {
        this.backLayout = backLayout;
    }

    @Override
    public void onStart() {
        super.onStart();
        model = (M) getArguments().getSerializable(getModelName());
        isEdit = model.getId() != null && model.getId() != 0;

        backLayout = getArguments().getInt(getString(R.string.back), backLayout);
    }

    public M getModel() {
        return model;
    }

    public void setModel(M model) {
        this.model = model;
    }

    public boolean isEdit() {
        return isEdit;
    }
}
