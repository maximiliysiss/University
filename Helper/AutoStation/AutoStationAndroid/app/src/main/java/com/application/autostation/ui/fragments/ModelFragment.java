package com.application.autostation.ui.fragments;

import android.app.Activity;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import androidx.fragment.app.Fragment;

import com.application.autostation.R;
import com.application.autostation.ui.models.Model;

public abstract class ModelFragment<T extends Model, A extends Activity> extends Fragment {

    T obj;

    public T getObj() {
        return obj;
    }


    public A getRealActivity() {
        return (A) getActivity();
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
                if (loadObject())
                    add(obj);
                else
                    Toast.makeText(getContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            });
        } else {
            delete.setVisibility(View.VISIBLE);
            delete.setOnClickListener(v -> delete(obj.getId()));
            action.setText("Изменить");
            action.setOnClickListener(v -> {
                if (loadObject())
                    update(obj);
                else
                    Toast.makeText(getContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            });
        }

    }

    public abstract boolean loadObject();

    public abstract void add(T obj);

    public abstract void delete(int id);

    public abstract void update(T obj);
}
