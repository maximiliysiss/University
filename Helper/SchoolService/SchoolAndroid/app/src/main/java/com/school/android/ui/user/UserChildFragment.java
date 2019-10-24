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
import com.school.android.threadable.Future;
import com.school.android.threadable.ThreadResult;
import com.school.android.ui.adapters.spinner.SpinnerCustomAdapter;

import java.io.IOException;
import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserChildFragment extends UserDefaultFragment {


    Spinner classes;

    public UserChildFragment() {
        layout = R.layout.fragment_user_child;
    }

    @Override
    public void onStart() {
        super.onStart();

        List<Class> classList = new Future<>(() -> App.getClassRetrofit().getModels().execute().body()).get();
        classList.add(0, new Class());

        classes = getView().findViewById(R.id.current_class);
        classes.setAdapter(new SpinnerCustomAdapter<Class>(classList, R.layout.spinner_item, getContext()) {
            @Override
            protected String getModelName(Class el) {
                return el.getName();
            }
        });

    }
}
