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
import com.school.android.network.classes.UniversalCallback;
import com.school.android.network.classes.UniversalWithCodeCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.spinner.SpinnerCustomAdapter;
import com.school.android.ui.fragments.ModelActionFragment;
import com.school.android.utilities.NetworkUtilities;

import java.util.Arrays;
import java.util.stream.Collectors;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserElementFragment extends ModelActionFragment<MainActivity, User> {


    FrameLayout frameLayout;
    Spinner spinner;

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

    @Override
    public void onStart() {
        super.onStart();

        spinner = getView().findViewById(R.id.user_type);
        frameLayout = getView().findViewById(R.id.user_frame);

        UserType[] userTypes = UserType.values();

        spinner.setAdapter(new SpinnerCustomAdapter<UserType>(Arrays.stream(userTypes).collect(Collectors.toList()), R.layout.spinner_item, getContext()) {
            @Override
            protected String getModelName(UserType el) {
                //return "Hello";
                return getString(new StringBuilder("enum_").append(el.toString().toLowerCase()).toString());
            }
        });

        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                UserType type = userTypes[position];
                Fragment fragment = new UserDefaultFragment();
                setModel(new User());
                switch (type) {
                    case Teacher:
                        fragment = new UserTeacherFragment();
                        setModel(new Teacher());
                        break;
                    case Student:
                        fragment = new UserChildFragment();
                        setModel(new Children());
                        break;
                }

                getModel().setUserType(type.ordinal());
                Bundle bundle = new Bundle();
                bundle.putSerializable(getModelName(), getModel());
                fragment.setArguments(bundle);
                openFragment(R.id.user_frame, fragment);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        spinner.setSelection(getSpinnerIndex(spinner, UserType.values()[getModel().getUserType()]));
        generateModelActions(getView());
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }

    public int getSpinnerIndex(Spinner spinner, UserType userType) {
        for (int i = 0; i < spinner.getCount(); i++) {
            if (spinner.getItemAtPosition(i) == userType)
                return i;
        }
        return 0;
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
    public void onDelete(User user) {
        App.getUserRetrofit().delete(user.getId());
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

    EditText surname;
    EditText name;
    EditText secondName;
    EditText phone;
    EditText passport;
    EditText email;

    @Override
    public void loadModel() {
        View view = getView();

        surname = view.findViewById(R.id.surname);
        name = view.findViewById(R.id.username);
        secondName = view.findViewById(R.id.second_name);
        phone = view.findViewById(R.id.phone);
        passport = view.findViewById(R.id.passport);
        email = view.findViewById(R.id.email);


        getModel().setEmail(email.getText().toString().trim());
        getModel().setName(name.getText().toString().trim());
        getModel().setSecondName(secondName.getText().toString().trim());
        getModel().setSurname(surname.getText().toString().trim());
        getModel().setPhone(phone.getText().toString().trim());
        getModel().setPassport(passport.getText().toString().trim());

        switch (UserType.values()[getModel().getUserType()]) {
            case Student: {

                Spinner classes = getView().findViewById(R.id.current_class);
                Class aClass = (Class) classes.getSelectedItem();
                if (aClass.getId() != 0)
                    ((Children) getModel()).setClass_(aClass);
                break;
            }
        }
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
                App.getClassRetrofit().setTeacher(aClass.getId());
        }
    }
}
