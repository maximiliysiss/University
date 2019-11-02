package com.school.android.ui.adapters.spinner;

import android.content.Context;

import com.school.android.R;
import com.school.android.models.network.input.Children;

import java.util.List;

public class ChildrenSpinnerAdapter extends SpinnerCustomModelAdapter<Children> {
    public ChildrenSpinnerAdapter(List<Children> data, Context context) {
        super(data, R.layout.spinner_item, context);
    }

    @Override
    public String getText(Children obj) {
        return obj.getFullName();
    }
}
