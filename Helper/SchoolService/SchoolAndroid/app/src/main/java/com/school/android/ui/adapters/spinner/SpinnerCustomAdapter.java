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

    List<T> list;
    int itemLayout;
    Context context;

    public SpinnerCustomAdapter(List<T> list, int itemLayout, Context context) {
        this.list = list;
        this.itemLayout = itemLayout;
        this.context = context;
    }

    @Override
    public int getCount() {
        return list.size();
    }

    @Override
    public Object getItem(int position) {
        return list.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View view = View.inflate(context, itemLayout, null);
        TextView textView = view.findViewById(R.id.name);
        textView.setText(getModelName(list.get(position)));
        return view;
    }

    protected abstract String getModelName(T el);
}
