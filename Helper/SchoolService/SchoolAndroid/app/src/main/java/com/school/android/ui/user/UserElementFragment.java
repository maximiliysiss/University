package com.school.android.ui.user;


import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.Spinner;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.Teacher;
import com.school.android.models.network.input.User;
import com.school.android.network.classes.EmptyResult;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.spinner.SpinnerCustomAdapter;
import com.school.android.ui.fragments.ModelActionFragment;
import com.school.android.ui.fragments.ModelFragment;

import java.util.Arrays;
import java.util.stream.Collectors;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserElementFragment extends ModelActionFragment<MainActivity, User> {


    FrameLayout frameLayout;
    Spinner spinner;
    NavController navController;

    public UserElementFragment() {
        super(R.id.navigation_users);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_user_element, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        spinner = getView().findViewById(R.id.user_type);
        frameLayout = getView().findViewById(R.id.user_frame);
        navController = Navigation.findNavController(this.getActivity(), R.id.user_frame);

        UserType[] userTypes = UserType.values();

        spinner.setAdapter(new SpinnerCustomAdapter<UserType>(Arrays.stream(userTypes).collect(Collectors.toList()), R.layout.spinner_item, getContext()) {
            @Override
            protected String getModelName(UserType el) {
                return getString(new StringBuilder("elem_").append(el.toString().toLowerCase()).toString());
            }
        });

        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                UserType type = userTypes[position];
                int layoutId = R.layout.fragment_user_default;
                setModel(new User());
                switch (type) {
                    case Teacher:
                        layoutId = R.layout.fragment_user_teacher;
                        setModel(new Teacher());
                        break;
                    case Student:
                        layoutId = R.layout.fragment_user_child;
                        setModel(new Children());
                        break;
                }

                Bundle bundle = new Bundle();
                bundle.putSerializable(getModelName(), getModel());
                navController.navigate(layoutId, bundle);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        spinner.setSelection(getSpinnerIndex(spinner, UserType.values()[getModel().getUserType()]));
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
                App.getTeacherRetrofit().update(user.getId(), (Teacher) user).enqueue(new EmptyResult<>(getContext()));
                break;
            case Student:
                App.getChildrenRetrofit().update(user.getId(), (Children) user).enqueue(new EmptyResult<>(getContext()));
                break;
            default:
                App.getUserRetrofit().update(user.getId(), user).enqueue(new EmptyResult<>(getContext()));
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
                App.getTeacherRetrofit().create((Teacher) user).enqueue(new EmptyResult<>(getContext()));
                break;
            case Student:
                App.getChildrenRetrofit().create((Children) user).enqueue(new EmptyResult<>(getContext()));
                break;
            default:
                App.getUserRetrofit().create(user).enqueue(new EmptyResult<>(getContext()));
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
        name = view.findViewById(R.id.name);
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
    }
}
