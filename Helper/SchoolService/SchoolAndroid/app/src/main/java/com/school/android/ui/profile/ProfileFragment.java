package com.school.android.ui.profile;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.User;
import com.school.android.models.network.output.ChangeUser;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;

public class ProfileFragment extends Fragment {

    EditText password;
    EditText login;
    EditText passwordConfirm;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_profile, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        password = getView().findViewById(R.id.password);
        login = getView().findViewById(R.id.login);
        passwordConfirm = getView().findViewById(R.id.password_confirm);

        Button button = getView().findViewById(R.id.change_user);
        button.setOnClickListener(v -> {
            String passwordString = passwordConfirm.getText().toString().trim();
            if (passwordString.length() == 0) {
                Toast.makeText(getContext(), getString(R.string.set_password), Toast.LENGTH_SHORT).show();
                return;
            }

            App.getAuthRetrofit().changeUser(new ChangeUser(login.getText().toString().trim(), password.getText().toString().trim(), passwordString))
                    .enqueue(new UniversalCallback<>(getContext(), () -> ((MainActivity) getActivity()).openFragment(R.id.navigation_profile)));
        });

        App.getUserRetrofit().getModel(App.getUserContext().id).enqueue(new UniversalCallback<>(getContext(), x -> {
            login.setText(x.getLogin());
        }));
    }
}