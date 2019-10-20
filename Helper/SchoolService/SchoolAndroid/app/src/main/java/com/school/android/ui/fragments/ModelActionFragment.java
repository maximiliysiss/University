package com.school.android.ui.fragments;

import android.view.View;
import android.widget.Button;

import com.school.android.R;
import com.school.android.models.network.FragmentModel;
import com.school.android.ui.activity.ActivityFragmenter;

public abstract class ModelActionFragment<T extends ActivityFragmenter, M extends FragmentModel> extends ModelFragment<T, M> {


    int layoutBase;

    public ModelActionFragment(int layoutBase) {
        this.layoutBase = layoutBase;
    }

    public void generateModelActions(View view) {
        Button action = view.findViewById(R.id.action);
        Button delete = view.findViewById(R.id.delete);

        if (isEdit) {
            action.setText(getString(R.string.edit));
            action.setOnClickListener(v -> {
                loadModel();
                onSave(getModel());
                toBackBaseFragment();
            });

            delete.setVisibility(View.VISIBLE);
            delete.setText(getString(R.string.delete));
            delete.setOnClickListener(v -> {
                loadModel();
                onDelete(getModel());
                toBackBaseFragment();
            });

        } else {
            delete.setVisibility(View.INVISIBLE);
            action.setText(getString(R.string.add));
            action.setOnClickListener(v -> {
                loadModel();
                onAdd(getModel());
                toBackBaseFragment();
            });
        }
    }

    public abstract void onSave(M m);

    public abstract void onDelete(M m);

    public abstract void onAdd(M m);

    public abstract void loadModel();

    public void toBackBaseFragment() {
        getRealActivity().openFragment(layoutBase);
    }
}
