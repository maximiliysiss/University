package com.school.android.ui.fragments;

import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.recyclerview.widget.RecyclerView;

import com.school.android.R;
import com.school.android.models.network.FragmentModel;
import com.school.android.ui.activity.ActivityFragmenter;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapterConstructor;
import com.school.android.ui.adapters.recyclerview.ViewHolders.RecyclerHolder;
import com.school.android.ui.callbacks.FilterAction;
import com.school.android.ui.callbacks.TextWatcherCallback;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;

public abstract class ModelActionFragment<T extends ActivityFragmenter, M extends FragmentModel> extends ModelFragment<T, M> {

    public enum Operation {
        Save,
        Add,
        Delete
    }

    public ModelActionFragment(int backLayout) {
        super(backLayout);
    }

    public void hideActions() {
        Button action = getView().findViewById(R.id.action);
        action.setVisibility(View.INVISIBLE);
    }

    public void generateModelActions(View view) {
        Button action = view.findViewById(R.id.action);
        Button delete = view.findViewById(R.id.delete);

        if (isEdit) {
            action.setText(getString(R.string.edit));
            action.setOnClickListener(v -> {
                if (loadModel())
                    onSave(getModel());
                else
                    Toast.makeText(getContext(), getString(R.string.not_enter), Toast.LENGTH_SHORT).show();
            });

            delete.setVisibility(View.VISIBLE);
            delete.setText(getString(R.string.delete));
            delete.setOnClickListener(v -> {
                onDelete(getModel().getId());
            });

        } else {
            delete.setVisibility(View.INVISIBLE);
            action.setText(getString(R.string.add));
            action.setOnClickListener(v -> {
                if (loadModel())
                    onAdd(getModel());
                else
                    Toast.makeText(getContext(), getString(R.string.not_enter), Toast.LENGTH_SHORT).show();
            });
        }
    }

    public void endOperation(int code, Operation operation, M m) {
        afterOperation(operation, m);
        toBackBaseFragment();
    }

    public void endOperation(int code, M m) {
        afterOperation(Operation.Add, m);
        toBackBaseFragment();
    }

    public void endOperation(M m) {
        afterOperation(Operation.Add, m);
        toBackBaseFragment();
    }

    public abstract void onSave(M m);

    public abstract void onDelete(int id);

    public abstract void onAdd(M m);

    public abstract boolean loadModel();

    public void afterOperation(Operation operation, M m) {

    }

    public void toBackBaseFragment() {
        getRealActivity().openFragment(backLayout);
    }
}
