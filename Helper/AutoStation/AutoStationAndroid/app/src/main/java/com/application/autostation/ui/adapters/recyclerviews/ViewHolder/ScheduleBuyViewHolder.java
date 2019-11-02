package com.application.autostation.ui.adapters.recyclerviews.ViewHolder;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;

import androidx.annotation.NonNull;

import com.application.autostation.R;
import com.application.autostation.ui.activities.PayActivity;

/**
 * Описание элемента RecyclerView покупки
 */
public class ScheduleBuyViewHolder extends ScheduleViewHolder {

    public ScheduleBuyViewHolder(@NonNull View itemView) {
        super(itemView);
    }

    /**
     * Обработка нажатия
     */
    @Override
    public void onClick() {
        Intent intent = new Intent(getActivity(), PayActivity.class);
        intent.putExtra(getString(R.string.schedule_model), object);
        getActivity().startActivity(intent);
    }
}
