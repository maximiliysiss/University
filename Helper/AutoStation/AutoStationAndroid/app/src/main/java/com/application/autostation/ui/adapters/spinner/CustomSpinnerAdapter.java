package com.application.autostation.ui.adapters.spinner;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.SpinnerAdapter;
import android.widget.TextView;

import com.application.autostation.R;

import java.util.List;

public abstract class CustomSpinnerAdapter<T> extends BaseAdapter implements SpinnerAdapter {
    List<T> data;
    int layout;
    Context context;

    public CustomSpinnerAdapter(List<T> data, int layout, Context context) {
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
        for (int i = 0; i < data.size(); i++)
            if (obj.equals(obj))
                return i;
        return -1;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View view = View.inflate(context, layout, null);
        TextView textView = view.findViewById(R.id.name);
        textView.setText(getText(data.get(position)));
        return null;
    }

    public abstract String getText(T obj);
}
