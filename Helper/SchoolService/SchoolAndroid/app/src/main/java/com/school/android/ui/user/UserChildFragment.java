package com.school.android.ui.user;


import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.ChildInRiskGroup;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.RiskGroup;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.threadable.Future;
import com.school.android.threadable.ThreadResult;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.spinner.ClassSpinnerAdapter;
import com.school.android.ui.adapters.spinner.SpinnerCustomAdapter;
import com.school.android.ui.fragments.ModelFragment;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

/**
 * A simple {@link Fragment} subclass.
 */
public class UserChildFragment extends ModelFragment<MainActivity, Children> {


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
    Button toArchive;
    Button addRiskGroup;

    public UserChildFragment() {
        layout = R.layout.fragment_user_child;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(layout, container, false);
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
        classes = getView().findViewById(R.id.current_class);
        App.getClassRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            classes.setAdapter(new ClassSpinnerAdapter(x, getContext()));
        }));

        toArchive = getView().findViewById(R.id.to_archive);
        addRiskGroup = getView().findViewById(R.id.add_to_risk_group);


        if (getModel().getId() != 0) {
            toArchive.setOnClickListener(v -> App.getChildrenRetrofit().archive(getModel().getId()).enqueue(new UniversalCallback<>(getContext(), x -> {
                getRealActivity().openFragment(R.id.navigation_users);
            })));

            if (getModel().getIsArchive())
                toArchive.setText(getString(R.string.from_archive));

            addRiskGroup.setOnClickListener(v -> App.getRiskGroupRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
                ArrayList<String> names = new ArrayList<>();
                for (RiskGroup riskGroup : x) {
                    boolean flag = true;
                    for (ChildInRiskGroup group : riskGroup.getChildInRiskGroups()) {
                        if (group.getChildId() == getModel().getId()) {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) {
                        names.add(riskGroup.getName());
                    }
                }

                AlertDialog.Builder builder = new AlertDialog.Builder(getContext()).setTitle(R.string.select_risk_group)
                        .setItems(names.toArray(new String[names.size()]), (dialog, which) -> App.getRiskGroupRetrofit().addChildToRiskGroup(getModel().getId(), x.get(which).getId()).enqueue(new UniversalCallback<>(getContext(), z -> {
                        })));

                builder.create().show();
            })));
        }else{
            toArchive.setVisibility(View.INVISIBLE);
            addRiskGroup.setVisibility(View.INVISIBLE);
        }
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }
}
