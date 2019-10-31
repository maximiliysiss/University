package com.application.autostation.ui.profile;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.User;
import com.application.autostation.network.models.output.ChangeUser;
import com.application.autostation.ui.activities.AdminActivity;

/**
 * A simple {@link Fragment} subclass.
 */
public class ProfileFragment extends Fragment {


    public ProfileFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_profile, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        EditText password = getView().findViewById(R.id.password);
        EditText repeat = getView().findViewById(R.id.repeate);
        EditText login = getView().findViewById(R.id.login);

        App.getUserRetrofit().getUser().enqueue(new UniversalCallback<>(getContext(), x -> {
            login.setText(x.getLogin());
        }));

        Button change = getView().findViewById(R.id.change);
        change.setOnClickListener(v -> {
            String passwordString = repeat.getText().toString().trim();
            if (passwordString.length() == 0) {
                Toast.makeText(getContext(), "Введите текущий пароль", Toast.LENGTH_SHORT).show();
                return;
            }

            App.getUserRetrofit().changeUser(new ChangeUser(login.getText().toString().trim(),
                    password.getText().toString().trim(), passwordString)).enqueue(new UniversalCallback<>(getContext(), () -> {
                ((AdminActivity) getActivity()).openFragment(R.id.navigation_profile);
            }));
        });
    }
}
