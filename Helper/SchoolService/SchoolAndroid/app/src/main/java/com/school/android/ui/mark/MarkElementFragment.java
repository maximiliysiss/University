package com.school.android.ui.mark;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.Spinner;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Mark;
import com.school.android.network.classes.UniversalWithCodeCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.fragments.ModelActionFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class MarkElementFragment extends ModelActionFragment<MainActivity, Mark> {


    EditText mark;
    Spinner className;
    Spinner day;
    Spinner student;
    Spinner lesson;

    public MarkElementFragment() {
        super(R.id.navigation_marks);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_mark_element, container, false);
    }

    @Override
    public void onSave(Mark mark) {
        App.getMarkRetrofit().update(mark.getId(), mark).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public void onDelete(int id) {
        App.getMarkRetrofit().delete(id).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public void onAdd(Mark mark) {
        App.getMarkRetrofit().create(mark).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public boolean loadModel() {
        return true;
    }

    @Override
    public String getModelName() {
        return getString(R.string.mark_model);
    }
}
