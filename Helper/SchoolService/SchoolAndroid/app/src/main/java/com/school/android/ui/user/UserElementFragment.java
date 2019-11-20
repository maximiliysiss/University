package com.school.android.ui.user;


import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.Spinner;

import androidx.fragment.app.Fragment;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Teacher;
import com.school.android.models.network.input.User;
import com.school.android.network.classes.UniversalWithCodeCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.spinner.UserTypeSpinnerAdapter;
import com.school.android.ui.fragments.ModelActionFragment;
import com.school.android.utilities.NetworkUtilities;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.stream.Collectors;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserElementFragment extends ModelActionFragment<MainActivity, User> {


    FrameLayout frameLayout;
    Spinner spinner;
    private boolean withoutStudent;

    public UserElementFragment() {
        super(R.id.navigation_users);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_user_element, container, false);
    }

    public void openFragment(int id, Fragment fragment) {
        this.getActivity().getSupportFragmentManager().beginTransaction().replace(id, fragment).commit();
    }

    EditText surname;
    EditText name;
    EditText secondName;
    EditText phone;
    EditText passport;
    EditText email;
    EditText login;
    EditText password;
    EditText birthday;

    @Override
    public void onStart() {
        super.onStart();

        spinner = getView().findViewById(R.id.user_type);
        frameLayout = getView().findViewById(R.id.user_frame);

        UserType[] userTypes = UserType.values();
        this.withoutStudent = getArguments().getBoolean(getString(R.string.without_student), false);
        if (withoutStudent) {
            ArrayList<UserType> arrayList = (ArrayList<UserType>) Arrays.asList(userTypes).stream().filter(x -> x != UserType.Student).collect(Collectors.toList());
            userTypes = arrayList.toArray(new UserType[arrayList.size()]);
        }

        if (getArguments().getBoolean(getString(R.string.only_student), false))
            spinner.setVisibility(View.INVISIBLE);

        UserTypeSpinnerAdapter userTypeSpinnerAdapter = new UserTypeSpinnerAdapter(Arrays.stream(userTypes).collect(Collectors.toList()), getContext());
        spinner.setAdapter(userTypeSpinnerAdapter);

        UserType[] finalUserTypes = userTypes;
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                UserType type = finalUserTypes[position];
                boolean isChange = type.ordinal() != getModel().getUserType();
                Fragment fragment = new UserDefaultFragment(backLayout);
                setModel(isChange ? new User() : getModel());
                switch (type) {
                    case Teacher:
                        fragment = new UserTeacherFragment(backLayout);
                        setModel(isChange ? new Teacher() : getModel());
                        break;
                    case Student:
                        fragment = new UserChildFragment(backLayout);
                        setModel(isChange ? new Children() : getModel());
                        break;
                }

                getModel().setUserType(type.ordinal());
                Bundle bundle = new Bundle();
                bundle.putBoolean(getString(R.string.without_student), withoutStudent);
                bundle.putSerializable(getModelName(), getModel());
                fragment.setArguments(bundle);
                openFragment(R.id.user_frame, fragment);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        spinner.setSelection(userTypeSpinnerAdapter.getIndex(UserType.values()[getModel().getUserType()]));
        if (App.getUserType() != UserType.Social)
            generateModelActions(getView());
        else
            hideActions();
    }


    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }

    @Override
    public void onSave(User user) {
        switch (UserType.values()[user.getUserType()]) {
            case Teacher:
                App.getTeacherRetrofit().update(user.getId(), (Teacher) user).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, object) -> endOperation(c, Operation.Add, object)));
                break;
            case Student:
                App.getChildrenRetrofit().update(user.getId(), (Children) user).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, object) -> endOperation(c, Operation.Add, object)));
                break;
            default:
                App.getUserRetrofit().update(user.getId(), user).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, object) -> endOperation(c, Operation.Add, object)));
                break;
        }
    }

    @Override
    public void onDelete(int id) {
        App.getUserRetrofit().delete(id).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Delete, v)));
    }

    @Override
    public void onAdd(User user) {
        switch (UserType.values()[user.getUserType()]) {
            case Teacher:
                App.getTeacherRetrofit().create((Teacher) user).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, object) -> endOperation(c, Operation.Add, object)));
                break;
            case Student:
                App.getChildrenRetrofit().create((Children) user).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, object) -> endOperation(c, Operation.Add, object)));
                break;
            default:
                App.getUserRetrofit().create(user).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, object) -> endOperation(c, Operation.Add, object)));
                break;
        }
    }

    @Override
    public boolean loadModel() {
        View view = getView();

        surname = view.findViewById(R.id.surname);
        name = view.findViewById(R.id.username);
        secondName = view.findViewById(R.id.second_name);
        phone = view.findViewById(R.id.phone);
        passport = view.findViewById(R.id.passport);
        email = view.findViewById(R.id.email);
        login = view.findViewById(R.id.login);
        password = view.findViewById(R.id.password);
        birthday = view.findViewById(R.id.birthday);

        String nameString = name.getText().toString().trim();
        String surnameString = surname.getText().toString().trim();
        String secondNameString = secondName.getText().toString().trim();
        String phoneString = phone.getText().toString().trim();
        String passportString = passport.getText().toString().trim();
        String emailString = email.getText().toString().trim();
        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();
        String birthdayString = birthday.getText().toString().trim();

        if (nameString.length() == 0 || surnameString.length() == 0 || secondNameString.length() == 0 || phoneString.length() == 0 || passportString.length() == 0
                || emailString.length() == 0 || loginString.length() == 0 || passwordString.length() == 0 || birthdayString.length() == 0) {
            return false;
        }

        Spinner classes = getView().findViewById(R.id.current_class);
        if (classes != null && classes.getCount() == 0)
            return false;

        getModel().setEmail(emailString);
        getModel().setName(nameString);
        getModel().setSecondName(secondNameString);
        getModel().setSurname(surnameString);
        getModel().setPhone(phoneString);
        getModel().setPassport(passportString);
        getModel().setLogin(loginString);
        getModel().setPasswordHash(passwordString);
        getModel().setBirthday(birthdayString);

        switch (UserType.values()[getModel().getUserType()]) {
            case Student: {
                Class aClass = (Class) classes.getSelectedItem();
                if (aClass.getId() != 0) {
                    Children children = (Children) getModel();
                    children.setClassId(aClass.getId());
                    children.setClass_(null);
                }
                break;
            }
        }

        return true;
    }

    @Override
    public void endOperation(int code, Operation operation, User user) {
        if (NetworkUtilities.isSuccess(code)) {
            super.endOperation(code, operation, user);
        }
    }

    @Override
    public void afterOperation(Operation operation, User user) {
        super.afterOperation(operation, user);

        if (getModel().getUserType() == UserType.Teacher.ordinal()) {
            Spinner classes = getView().findViewById(R.id.current_class);
            Class aClass = (Class) classes.getSelectedItem();
            if (aClass.getId() != 0)
                App.getClassRetrofit().setTeacher(aClass.getId(), getModel().getId());
        }
    }
}
