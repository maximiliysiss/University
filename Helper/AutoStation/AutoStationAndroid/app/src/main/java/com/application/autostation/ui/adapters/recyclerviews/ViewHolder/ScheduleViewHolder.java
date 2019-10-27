package com.application.autostation.ui.adapters.recyclerviews.ViewHolder;

import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.application.autostation.R;
import com.application.autostation.network.models.input.Schedule;
import com.application.autostation.ui.activities.AdminActivity;
import com.application.autostation.utilities.DayOfWeek;

public class ScheduleViewHolder extends RecyclerViewHolder<Schedule> {

    TextView from;
    TextView to;
    TextView price;
    TextView time;
    TextView distance;
    TextView day;

    public ScheduleViewHolder(@NonNull View itemView) {
        super(itemView);

        from = itemView.findViewById(R.id.from);
        to = itemView.findViewById(R.id.to);
        price = itemView.findViewById(R.id.price);
        time = itemView.findViewById(R.id.time);
        distance = itemView.findViewById(R.id.distance);
        day = itemView.findViewById(R.id.day);
    }

    @Override
    public void onClick() {
        ((AdminActivity) getActivity()).openFragment(R.id.navigation_schedule, getString(R.string.schedule_model), object);
    }

    @Override
    public void setObject(Schedule object) {
        super.setObject(object);

        from.setText(object.getFrom().getName());
        to.setText(object.getTo().getName());
        price.setText(String.valueOf(object.getPrice()));
        time.setText(object.getTime());
        distance.setText(String.valueOf(object.getDistance()));
        day.setText(DayOfWeek.getDay(object.getDayOfWeek()));
    }
}
