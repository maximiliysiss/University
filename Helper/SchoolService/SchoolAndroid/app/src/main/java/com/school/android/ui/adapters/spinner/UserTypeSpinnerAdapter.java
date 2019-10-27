package com.school.android.ui.adapters.spinner;

import android.content.Context;

import com.school.android.R;
import com.school.android.models.extension.UserType;

import java.util.List;

public class UserTypeSpinnerAdapter extends SpinnerCustomAdapter<UserType> {
    public UserTypeSpinnerAdapter(List<UserType> data, Context context) {
        super(data, R.layout.spinner_item, context);
    }

    @Override
    public String getText(UserType obj) {
        return obj.toString();
    }
}
