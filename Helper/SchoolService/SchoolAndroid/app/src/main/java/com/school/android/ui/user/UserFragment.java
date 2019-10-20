package com.school.android.ui.user;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.school.android.R;
import com.school.android.models.network.input.User;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.fragments.BaseFragment;
import com.school.android.ui.fragments.ModelContainsFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserFragment extends ModelContainsFragment<MainActivity> {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_user, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
        Button button = getView().findViewById(R.id.add);
        button.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_users_element, getModelName(), new User()));
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }
}
