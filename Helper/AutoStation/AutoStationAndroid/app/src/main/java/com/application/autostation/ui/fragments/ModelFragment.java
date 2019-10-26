package com.application.autostation.ui.fragments;

import android.view.View;
import android.widget.Button;

import androidx.fragment.app.Fragment;

import com.application.autostation.R;
import com.application.autostation.ui.models.Model;

public abstract class ModelFragment<T extends Model> extends Fragment {

    T obj;

    public T getObj() {
        return obj;
    }


    @Override
    public void onStart() {
        super.onStart();

        obj = (T) getArguments().getSerializable(getModelName());
    }

    public abstract String getModelName();

    public void generateActions() {

        View view = getView();

        Button action = view.findViewById(R.id.action);
        Button delete = view.findViewById(R.id.delete);

        if (obj.getId() == 0) {
            action.setText("Добавить");
            action.setOnClickListener(v -> {
                loadObject();
                add(obj);
            });
        } else {
            delete.setVisibility(View.VISIBLE);
            delete.setOnClickListener(v -> delete(obj.getId()));
            action.setText("Изменить");
            action.setOnClickListener(v -> {
                loadObject();
                update(obj);
            });
        }

    }

    public abstract void loadObject();

    public abstract void add(T obj);

    public abstract void delete(int id);

    public abstract void update(T obj);
}
