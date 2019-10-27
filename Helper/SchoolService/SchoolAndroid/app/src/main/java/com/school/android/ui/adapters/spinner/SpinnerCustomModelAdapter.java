package com.school.android.ui.adapters.spinner;

import android.content.Context;

import com.school.android.models.network.FragmentModel;

import java.util.List;

public abstract class SpinnerCustomModelAdapter<T extends FragmentModel> extends SpinnerCustomAdapter<T> {
    public SpinnerCustomModelAdapter(List<T> data, int layout, Context context) {
        super(data, layout, context);
    }

    @Override
    public int getIndex(T obj) {
        if (obj == null)
            return 0;

        for (int i = 0; i < data.size(); i++) {
            if (obj.getId() == data.get(i).getId())
                return i;
        }
        return 0;
    }
}
