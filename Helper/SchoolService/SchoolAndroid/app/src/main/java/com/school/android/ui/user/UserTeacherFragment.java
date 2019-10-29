package com.school.android.ui.user;


import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.LessonProfile;
import com.school.android.models.network.input.Teacher;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.threadable.Future;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.ViewHolders.TeacherProfileViewHolder;
import com.school.android.ui.adapters.spinner.ClassSpinnerAdapter;
import com.school.android.ui.adapters.spinner.SpinnerCustomAdapter;
import com.school.android.ui.fragments.ModelFragment;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

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
    RecyclerView profiles;
    private boolean withoutStudent;

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
        this.withoutStudent = getArguments().getBoolean(getString(R.string.without_student), false);

        Button addToProfile = getView().findViewById(R.id.addPosition);
        addToProfile.setOnClickListener(v -> App.getLessonRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {

            ArrayList<Lesson> strings = new ArrayList<>();
            for (Lesson lesson : x) {
                boolean flag = true;
                for (LessonProfile lessonProfile : getModel().getLessonProfiles()) {
                    if (lesson.getId().equals(lessonProfile.getLessonId())) {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    strings.add(lesson);
            }

            AlertDialog.Builder builder = new AlertDialog.Builder(getContext()).setTitle(R.string.add_position)
                    .setItems(strings.stream().map(Lesson::getName).collect(Collectors.toList()).toArray(new String[strings.size()]), (dialog, which) -> {
                        App.getLessonRetrofit().setLessonProfile(strings.get(which).getId(), getModel().getId())
                                .enqueue(new UniversalCallback<>(getContext(), z -> {
                                    getRealActivity().openFragment(backLayout);
                                }));
                    });
            builder.create().show();
        })));

        surname = view.findViewById(R.id.surname);
        name = view.findViewById(R.id.username);
        secondName = view.findViewById(R.id.second_name);
        phone = view.findViewById(R.id.phone);
        passport = view.findViewById(R.id.passport);
        email = view.findViewById(R.id.email);
        birthday = view.findViewById(R.id.birthday);
        login = view.findViewById(R.id.login);
        profiles = view.findViewById(R.id.profiles);
        profiles.setLayoutManager(new LinearLayoutManager(getContext()));

        surname.setText(getModel().getSurname());
        name.setText(getModel().getName());
        phone.setText(getModel().getPhone());
        secondName.setText(getModel().getSecondName());
        passport.setText(getModel().getPassport());
        email.setText(getModel().getEmail());
        login.setText(getModel().getLogin());
        birthday.setText(getModel().getBirthday());


        profiles.setAdapter(new RecyclerViewAdapter(getModel().getLessonProfiles(), R.layout.recycler_lesson,
                v -> new TeacherProfileViewHolder(v, getString(R.string.lesson_model), getModel().getId(), withoutStudent,backLayout)));

        classes = getView().findViewById(R.id.current_class);
        App.getClassRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            classes.setAdapter(new ClassSpinnerAdapter(x, getContext()));
        }));

        if (!isEdit()) {
            addToProfile.setVisibility(View.INVISIBLE);
        }
    }

    @Override
    public String getModelName() {
        return getString(R.string.user_model);
    }
}
