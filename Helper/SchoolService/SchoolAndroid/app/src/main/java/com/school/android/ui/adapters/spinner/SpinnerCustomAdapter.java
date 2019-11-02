package com.school.android.ui.adapters.spinner;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.SpinnerAdapter;
import android.widget.TextView;

import com.school.android.R;

import java.util.List;

public abstract class SpinnerCustomAdapter<T> extends BaseAdapter implements SpinnerAdapter {

    List<T> data;
    int layout;
    Context context;

    public SpinnerCustomAdapter(List<T> data, int layout, Context context) {
        this.data = data;
        this.layout = layout;
        this.context = context;
    }

    @Override
    public int getCount() {
        return data.size();
    }

    @Override
    public T getItem(int position) {
        return data.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    public int getIndex(T obj) {
        if (obj == null)
            return 0;

        for (int i = 0; i < data.size(); i++)
            if (data.get(i).equals(obj))
                return i;
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View view = View.inflate(context, layout, null);
        TextView textView = view.findViewById(R.id.name);
        textView.setText(getText(data.get(position)));
        return view;
    }

    public abstract String getText(T obj);
}
