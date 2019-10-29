package com.school.android.ui.classes;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Teacher;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.network.classes.UniversalWithCodeCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.spinner.TeacherSpinnerAdapter;
import com.school.android.ui.fragments.ModelActionFragment;

import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */
public class ClassElementFragment extends ModelActionFragment<MainActivity, Class> {


    EditText name;
    CheckBox isStart;
    Spinner teachers;

    public ClassElementFragment() {
        super(R.id.navigation_classes);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_class_element, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
        name = getView().findViewById(R.id.name);
        isStart = getView().findViewById(R.id.isStart);
        teachers = getView().findViewById(R.id.teachers);
        name.setText(getModel().getName());
        isStart.setChecked(getModel().getStartClass());

        App.getTeacherRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            TeacherSpinnerAdapter teacherSpinnerAdapter = new TeacherSpinnerAdapter(x, R.layout.spinner_item, getContext());
            teachers.setAdapter(teacherSpinnerAdapter);
            teachers.setSelection(teacherSpinnerAdapter.getIndexById(getModel().getTeacherId()));
        }));

        generateModelActions(getView());
    }

    @Override
    public void onSave(Class aClass) {
        App.getClassRetrofit().update(aClass.getId(), aClass).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, x) -> endOperation(x)));
    }

    @Override
    public void onDelete(int id) {
        App.getClassRetrofit().delete(id).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, x) -> endOperation(x)));
    }

    @Override
    public void onAdd(Class aClass) {
        App.getClassRetrofit().create(aClass).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, x) -> endOperation(x)));
    }

    @Override
    public boolean loadModel() {

        String nameString = name.getText().toString().trim();
        if (nameString.length() == 0)
            return false;

        getModel().setName(nameString);
        getModel().setStartClass(isStart.isChecked());
        return true;
    }

    @Override
    public String getModelName() {
        return getString(R.string.class_model);
    }
}
