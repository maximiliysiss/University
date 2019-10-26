package com.application.autostation.ui.adapters.recyclerviews.ViewHolder;

import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.application.autostation.R;
import com.application.autostation.network.models.input.Point;
import com.application.autostation.ui.activities.AdminActivity;

public class PointViewHolder extends RecyclerViewHolder<Point> {

    TextView name;

    public PointViewHolder(@NonNull View itemView) {
        super(itemView);
        name = itemView.findViewById(R.id.name);
    }

    @Override
    public void onClick() {
        ((AdminActivity) getActivity()).openFragment(R.id.navigation_point, getString(R.string.point_model), object);
    }

    @Override
    public void setObject(Point object) {
        super.setObject(object);
        name.setText(object.getName());
    }
}
