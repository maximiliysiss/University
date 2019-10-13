package com.example.storage.ui.data.Spinner;

import android.content.Context;
import android.database.DataSetObserver;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.SpinnerAdapter;
import android.widget.TextView;

import com.example.storage.R;

import java.util.List;

/**
 * Класс для определения адаптера для простого выпадающего списка
 * @param <T>
 */
public abstract class CustomSpinnerAdapter<T> extends BaseAdapter implements SpinnerAdapter {

    /**
     * Данные
     */
    List<T> list;
    /**
     * Какой layout использовать для элементов
     */
    int itemLayout;
    /**
     * Контекст работы
     */
    Context context;

    public CustomSpinnerAdapter(List<T> list, int itemLayout, Context context) {
        this.list = list;
        this.itemLayout = itemLayout;
        this.context = context;
    }

    /**
     * Получить количество
     * @return
     */
    @Override
    public int getCount() {
        return list.size();
    }

    /**
     * Получить элемент
     * @param position
     * @return
     */
    @Override
    public Object getItem(int position) {
        return list.get(position);
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
     * Получить представление для элемента
     * @param position
     * @param convertView
     * @param parent
     * @return
     */
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View view = View.inflate(context, itemLayout, null);
        TextView textView = view.findViewById(R.id.name);
        textView.setText(getSpinnerText(list.get(position)));
        return view;
    }

    /**
     * Получить текст для элемента
     * @param elem
     * @return
     */
    protected abstract String getSpinnerText(T elem);
}
