package com.application.autostation.ui.adapters.spinner;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.SpinnerAdapter;
import android.widget.TextView;

import com.application.autostation.R;
import com.application.autostation.ui.models.Model;

import java.util.List;

/**
 * Адаптер для выпадающего списка с моделью
 * @param <T>
 */
public abstract class CustomModelSpinnerAdapter<T extends Model> extends CustomSpinnerAdapter<T> {

    public CustomModelSpinnerAdapter(List<T> data, int layout, Context context) {
        super(data, layout, context);
    }

    /**
     * Получить индекс
     * @param obj
     * @return
     */
    @Override
    public int getIndex(T obj) {
        if (obj == null)
            return 0;

        for (int i = 0; i < data.size(); i++)
            if (obj.getId() == data.get(i).getId())
                return i;
        return 0;
    }
}
