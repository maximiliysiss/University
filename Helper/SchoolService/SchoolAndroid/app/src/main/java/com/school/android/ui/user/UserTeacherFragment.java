package com.school.android.ui.user;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.Spinner;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Teacher;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.threadable.Future;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.spinner.ClassSpinnerAdapter;
import com.school.android.ui.adapters.spinner.SpinnerCustomAdapter;
import com.school.android.ui.fragments.ModelFragment;

import java.util.List;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserTeacherFragment extends ModelFragment<MainActivity, Teacher> {


    Spinner classes;
    int layout;
    EditText surname;
    EditText name;
    EditText secondName;
    EditText phone;
    EditText passport;
    EditText email;
    EditText birthday;
    EditText login;

    public UserTeacherFragment(int backLayout) {
        super(backLayout);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_user_teacher, container, false);
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


        classes = getView().findViewById(R.id.current_class);
        App.getClassRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            classes.setAdapter(new ClassSpinnerAdapter(x, getContext()));
        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }
}
