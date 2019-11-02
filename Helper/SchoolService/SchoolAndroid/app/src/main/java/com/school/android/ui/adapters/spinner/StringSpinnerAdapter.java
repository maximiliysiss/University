package com.school.android.ui.adapters.spinner;

import android.content.Context;

import java.util.List;

public class StringSpinnerAdapter extends SpinnerCustomAdapter<String> {
    public StringSpinnerAdapter(List<String> data, int layout, Context context) {
        super(data, layout, context);
    }

    @Override
    public String getText(String obj) {
        return obj;
    }
}
