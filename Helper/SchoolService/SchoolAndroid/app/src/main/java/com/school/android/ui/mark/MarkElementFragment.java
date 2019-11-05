package com.school.android.ui.mark;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.Mark;
import com.school.android.models.network.input.Schedule;
import com.school.android.network.classes.UniversalWithCodeCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.fragments.ModelActionFragment;
import com.school.android.ui.spinner.ClassSpinner;
import com.school.android.ui.spinner.DayCalendar;
import com.school.android.ui.spinner.LessonSpinner;
import com.school.android.ui.spinner.StudentSpinner;
import com.school.android.utilities.CustomDate;

import java.util.Calendar;

/**
 * A simple {@link Fragment} subclass.
 */
public class MarkElementFragment extends ModelActionFragment<MainActivity, Mark> {


    EditText mark;
    ClassSpinner className;
    DayCalendar day;
    StudentSpinner student;
    LessonSpinner lesson;

    public MarkElementFragment() {
        super(R.id.navigation_marks);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_mark_element, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        mark = getView().findViewById(R.id.mark);
        className = getView().findViewById(R.id.class_name);
        day = getView().findViewById(R.id.day);
        student = getView().findViewById(R.id.student);
        lesson = getView().findViewById(R.id.lesson);

        if (getModel().getId() != 0) {
            className.setObject(getModel().getSchedule().get_class());
            lesson.setObject(getModel().getSchedule());
            student.setObject(getModel().getChild());
            mark.setText(String.valueOf(getModel().getMarkReal()));
        }

        day.addObserver(className);
        className.addObserver(lesson);
        className.addObserver(student);
        lesson.setDaySpinner(day);

        if (getModel().getId() != 0) {
            CustomDate customDate = new CustomDate(getModel().getDateJson());
            day.setDate(customDate.toCalendar().getTime().getTime());
        } else
            day.setDate(Calendar.getInstance().getTime().getTime());
        day.setCustomDate(new CustomDate(day.getDate()));
        day.notifyObservers();
        generateModelActions(getView());
    }

    @Override
    public void onSave(Mark mark) {
        App.getMarkRetrofit().update(mark.getId(), mark).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public void onDelete(int id) {
        App.getMarkRetrofit().delete(id).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public void onAdd(Mark mark) {
        App.getMarkRetrofit().create(mark).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Save, v)));
    }

    @Override
    public boolean loadModel() {
        if (className.getCount() == 0 || lesson.getCount() == 0 || student.getCount() == 0)
            return false;
        String markString = mark.getText().toString().trim();
        if (markString.length() == 0)
            return false;

        CustomDate customDate = day.getCustomDate();

        getModel().setMarkReal(Integer.parseInt(markString));
        getModel().setSchedule(null);
        getModel().setDateJson(customDate.toString());
        getModel().setScheduleId(((Schedule) lesson.getSelectedItem()).getId());
        getModel().setChild(null);
        getModel().setChildId(((Children) student.getSelectedItem()).getId());
        getModel().setTeacherId(App.getUserContext().id);

        return true;
    }

    @Override
    public String getModelName() {
        return getString(R.string.mark_model);
    }
}
