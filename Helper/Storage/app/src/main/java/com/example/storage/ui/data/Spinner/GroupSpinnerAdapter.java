package com.example.storage.ui.data.Spinner;

import android.content.Context;

import com.example.storage.data.models.Group;

import java.util.List;

/**
 * Адаптер для выпадающего списка групп
 */
public class GroupSpinnerAdapter extends CustomSpinnerAdapter<Group> {
    public GroupSpinnerAdapter(List<Group> list, int itemLayout, Context context) {
        super(list, itemLayout, context);
    }

    @Override
    protected String getSpinnerText(Group elem) {
        return elem.getName();
    }
}
