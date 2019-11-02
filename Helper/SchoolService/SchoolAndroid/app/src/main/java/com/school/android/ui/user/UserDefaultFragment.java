package com.school.android.ui.user;


import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.fragment.app.Fragment;

import com.school.android.R;
import com.school.android.models.network.input.User;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.fragments.ModelFragment;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserDefaultFragment extends ModelFragment<MainActivity, User> {

    EditText surname;
    EditText name;
    EditText secondName;
    EditText phone;
    EditText passport;
    EditText email;
    EditText birthday;
    EditText login;

    public UserDefaultFragment(int backLayout) {
        super(backLayout);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_user_default, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        View view = getView();

        surname = view.findViewById(R.id.surname);
        name = view.findViewById(R.id.username);
        secondName = view.findViewById(R.id.second_name);
        phone = view.findViewById(R.id.phone);
        passport = view.findViewById(R.id.passport);
        email = view.findViewById(R.id.email);
        birthday = view.findViewById(R.id.birthday);
        login = view.findViewById(R.id.login);

        surname.setText(getModel().getSurname());
        name.setText(getModel().getName());
        phone.setText(getModel().getPhone());
        secondName.setText(getModel().getSecondName());
        passport.setText(getModel().getPassport());
        email.setText(getModel().getEmail());
        login.setText(getModel().getLogin());
        birthday.setText(getModel().getBirthday());
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }
}
