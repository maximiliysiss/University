package com.school.android.ui.adapters.spinner;

import android.content.Context;

import com.school.android.R;
import com.school.android.models.network.input.Class;

import java.util.List;

public class ClassSpinnerAdapter extends SpinnerCustomModelAdapter<Class> {
    public ClassSpinnerAdapter(List<Class> data, Context context) {
        super(data, R.layout.spinner_item, context);
    }

    @Override
    public String getText(Class obj) {
        return obj.getName();
    }
}
