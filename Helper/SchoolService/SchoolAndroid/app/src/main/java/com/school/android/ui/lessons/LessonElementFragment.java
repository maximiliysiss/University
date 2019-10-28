package com.school.android.ui.lessons;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Lesson;
import com.school.android.network.classes.UniversalWithCodeCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.fragments.ModelActionFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class LessonElementFragment extends ModelActionFragment<MainActivity, Lesson> {

    EditText name;

    public LessonElementFragment() {
        super(R.id.navigation_lessons);
    }

    @Override
    public void onStart() {
        super.onStart();

        name = getView().findViewById(R.id.name);
        name.setText(getModel().getName());

        generateModelActions(getView());
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_lesson_element, container, false);
    }

    @Override
    public void onSave(Lesson lesson) {
        App.getLessonRetrofit().update(lesson.getId(), lesson).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, v)));
    }

    @Override
    public void onDelete(int id) {
        App.getLessonRetrofit().delete(id).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, v)));
    }

    @Override
    public void onAdd(Lesson lesson) {
        App.getLessonRetrofit().create(lesson).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, v)));
    }

    @Override
    public boolean loadModel() {
        String nameString = name.getText().toString().trim();
        if (nameString.length() == 0)
            return false;
        getModel().setName(nameString);
        return true;
    }

    @Override
    public String getModelName() {
        return getString(R.string.lesson_model);
    }
}
