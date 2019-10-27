package com.school.android.ui.schedule;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.Schedule;
import com.school.android.models.network.input.Teacher;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.network.classes.UniversalWithCodeCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.spinner.ClassSpinnerAdapter;
import com.school.android.ui.adapters.spinner.StringSpinnerAdapter;
import com.school.android.ui.adapters.spinner.TeacherSpinnerAdapter;
import com.school.android.ui.fragments.ModelActionFragment;
import com.school.android.utilities.DayUtils;
import com.school.android.utilities.LessonUtils;

/**
 * A simple {@link Fragment} subclass.
 */
public class ScheduleElementFragment extends ModelActionFragment<MainActivity, Schedule> {

    Spinner teacher;
    Spinner classes;
    EditText lesson;
    Spinner day;
    Spinner number;
    CheckBox facultative;

    public ScheduleElementFragment() {
        super(R.id.navigation_schedules);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_schedule_element, container, false);
    }

    @Override
    public boolean loadModel() {
        String lessonString = lesson.getText().toString().trim();

        if (lessonString.length() == 0)
            return false;
        if (teacher.getCount() == 0 || classes.getCount() == 0 || day.getCount() == 0 || number.getCount() == 0)
            return false;

        getModel().set_class(null);
        getModel().setClassId(((Class) classes.getSelectedItem()).getId());
        getModel().setDayOfWeek(day.getSelectedItemPosition());
        getModel().setIsFacultative(facultative.isChecked());
        getModel().setLesson(lessonString);
        getModel().setLessonNumber(number.getSelectedItemPosition());
        getModel().setTeacherId(((Teacher) teacher.getSelectedItem()).getId());
        return true;
    }

    @Override
    public void onStart() {
        super.onStart();

        teacher = getView().findViewById(R.id.teacher);
        classes = getView().findViewById(R.id.class_name);
        lesson = getView().findViewById(R.id.lesson);
        day = getView().findViewById(R.id.day);
        number = getView().findViewById(R.id.time);
        facultative = getView().findViewById(R.id.facultative);

        App.getTeacherRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {

            TeacherSpinnerAdapter teacherSpinnerAdapter = new TeacherSpinnerAdapter(x, R.layout.spinner_item, getContext());
            teacher.setAdapter(teacherSpinnerAdapter);
            teacher.setSelection(teacherSpinnerAdapter.getIndexById(getModel().getTeacherId()));
        }));

        App.getClassRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {
            ClassSpinnerAdapter classSpinnerAdapter = new ClassSpinnerAdapter(x, getContext());
            classes.setAdapter(classSpinnerAdapter);
            classes.setSelection(classSpinnerAdapter.getIndexById(getModel().getClassId()));
        }));

        lesson.setText(getModel().getLesson());

        StringSpinnerAdapter daySpinner = new StringSpinnerAdapter(DayUtils.getStrings(), R.layout.spinner_item, getContext());
        day.setAdapter(daySpinner);
        day.setSelection(daySpinner.getIndex(DayUtils.getName(getModel().getDayOfWeek())));

        StringSpinnerAdapter numberAdapter = new StringSpinnerAdapter(LessonUtils.getStrings(), R.layout.spinner_item, getContext());
        number.setAdapter(numberAdapter);
        number.setSelection(numberAdapter.getIndex(LessonUtils.getStrings(getModel().getLessonNumber())));

        facultative.setChecked(getModel().getIsFacultative());

        generateModelActions(getView());
    }

    @Override
    public void onSave(Schedule schedule) {
        App.getScheduleRetrofit().update(schedule.getId(), schedule).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public void onDelete(int id) {
        App.getScheduleRetrofit().delete(id).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public void onAdd(Schedule schedule) {
        App.getScheduleRetrofit().create(schedule).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public String getModelName() {
        return getString(R.string.schedule_model);
    }
}
