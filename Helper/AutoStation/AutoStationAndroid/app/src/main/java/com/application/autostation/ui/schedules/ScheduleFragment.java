package com.application.autostation.ui.schedules;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.Point;
import com.application.autostation.network.models.input.Schedule;
import com.application.autostation.ui.activities.AdminActivity;
import com.application.autostation.ui.adapters.spinner.CustomSpinnerAdapter;
import com.application.autostation.ui.adapters.spinner.PointSpinnerAdapter;
import com.application.autostation.ui.fragments.ModelFragment;
import com.application.autostation.utilities.DayOfWeek;

/**
 * A simple {@link Fragment} subclass.
 */
public class ScheduleFragment extends ModelFragment<Schedule, AdminActivity> {

    Spinner from;
    Spinner to;
    EditText price;
    EditText time;
    EditText distance;
    Spinner day;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_schedule, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        from = getView().findViewById(R.id.from);
        to = getView().findViewById(R.id.to);
        price = getView().findViewById(R.id.price);
        time = getView().findViewById(R.id.time);
        distance = getView().findViewById(R.id.distance);
        day = getView().findViewById(R.id.day);

        App.getPointRetrofit().getPoints().enqueue(new UniversalCallback<>(getContext(), x -> {
            PointSpinnerAdapter pointSpinnerAdapter = new PointSpinnerAdapter(x, getContext());

            from.setAdapter(pointSpinnerAdapter);
            from.setSelection(pointSpinnerAdapter.getIndex(getObj().getFrom()));

            to.setAdapter(pointSpinnerAdapter);
            to.setSelection(pointSpinnerAdapter.getIndex(getObj().getTo()));
        }));

        price.setText(String.valueOf(getObj().getPrice()));
        time.setText(getObj().getTime());
        distance.setText(String.valueOf(getObj().getDistance()));

        CustomSpinnerAdapter<String> days = new CustomSpinnerAdapter<String>(DayOfWeek.getAllString(), R.layout.custom_spinner_item, getContext()) {
            @Override
            public String getText(String obj) {
                return obj;
            }
        };

        day.setAdapter(days);
        day.setSelection(days.getIndex(DayOfWeek.getDay(getObj().getDayOfWeek())));

    }

    @Override
    public String getModelName() {
        return getString(R.string.schedule_model);
    }

    @Override
    public boolean loadObject() {

        String distanceString = distance.getText().toString().trim();
        String priceString = price.getText().toString().trim();
        String timeString = time.getText().toString().trim();

        if (timeString.length() == 0 || priceString.length() == 0 || distanceString.length() == 0) {
            return false;
        }

        getObj().setDayOfWeek(day.getSelectedItemPosition());
        getObj().setDistance(Integer.parseInt(distanceString));
        getObj().setFrom(null);
        getObj().setFromId(((Point) from.getSelectedItem()).getId());
        getObj().setTo(null);
        getObj().setToId(((Point) to.getSelectedItem()).getId());
        getObj().setPrice(Double.parseDouble(priceString));
        getObj().setTime(timeString);
        return true;
    }

    @Override
    public void add(Schedule obj) {
        App.getScheduleRetrofit().create(getObj()).enqueue(new UniversalCallback<>(getContext(), x -> {
            getRealActivity().openFragment(R.id.navigation_schedules);
        }));
    }

    @Override
    public void delete(int id) {
        App.getScheduleRetrofit().delete(id).enqueue(new UniversalCallback<>(getContext(), x -> {
            getRealActivity().openFragment(R.id.navigation_schedules);
        }));
    }

    @Override
    public void update(Schedule obj) {
        App.getScheduleRetrofit().update(obj.getId(), obj).enqueue(new UniversalCallback<>(getContext(), x -> {
            getRealActivity().openFragment(R.id.navigation_schedules);
        }));
    }
}
