package com.school.android.ui.user;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Spinner;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.threadable.Future;
import com.school.android.ui.adapters.spinner.ClassSpinnerAdapter;
import com.school.android.ui.adapters.spinner.SpinnerCustomAdapter;

import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserTeacherFragment extends UserDefaultFragment {


    Spinner classes;

    public UserTeacherFragment() {
        layout = R.layout.fragment_user_teacher;
    }

    @Override
    public void onStart() {
        super.onStart();

        classes = getView().findViewById(R.id.current_class);
        App.getClassRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            x.add(0, new Class());
            classes.setAdapter(new ClassSpinnerAdapter(x, getContext()));
        }));
    }
}
