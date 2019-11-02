package com.application.autostation.ui.adapters.spinner;

import android.content.Context;

import com.application.autostation.R;
import com.application.autostation.network.models.input.Point;

import java.util.List;

/**
 * Адаптер выпадающего списка для точек
 */
public class PointSpinnerAdapter extends CustomModelSpinnerAdapter<Point> {
    public PointSpinnerAdapter(List<Point> data, Context context) {
        super(data, R.layout.custom_spinner_item, context);
    }

    @Override
    public String getText(Point point) {
        return point.getName();
    }
}
