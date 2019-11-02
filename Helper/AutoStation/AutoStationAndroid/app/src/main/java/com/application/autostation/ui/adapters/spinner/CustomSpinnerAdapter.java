package com.application.autostation.ui.adapters.spinner;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.SpinnerAdapter;
import android.widget.TextView;

import com.application.autostation.R;

import java.util.List;

/**
 * Адаптер выпадающего списка
 * @param <T>
 */
public abstract class CustomSpinnerAdapter<T> extends BaseAdapter implements SpinnerAdapter {
    /**
     * Данные
     */
    List<T> data;
    /**
     * В каком формате выводить
     */
    int layout;
    /**
     * Контекст
     */
    Context context;

    public CustomSpinnerAdapter(List<T> data, int layout, Context context) {
        this.data = data;
        this.layout = layout;
        this.context = context;
    }

    /**
     * Получить количетсов
     * @return
     */
    @Override
    public int getCount() {
        return data.size();
    }

    /**
     * Получить элемент
     * @param position
     * @return
     */
    @Override
    public T getItem(int position) {
        return data.get(position);
    }

    /**
     * Получить ID элемента
     * @param position
     * @return
     */
    @Override
    public long getItemId(int position) {
        return position;
    }

    /**
     * Получить индекс
     * @param obj
     * @return
     */
    public int getIndex(T obj) {
        if (obj == null)
            return 0;

        for (int i = 0; i < data.size(); i++)
            if (data.get(i).equals(obj))
                return i;
        return 0;
    }

    /**
     * Получить представление
     * @param position
     * @param convertView
     * @param parent
     * @return
     */
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View view = View.inflate(context, layout, null);
        TextView textView = view.findViewById(R.id.name);
        textView.setText(getText(data.get(position)));
        return view;
    }

    /**
     * Получить строку для вывода
     * @param obj
     * @return
     */
    public abstract String getText(T obj);
}
