package com.school.android.ui.spinner;

import android.content.Context;
import android.content.res.Resources;
import android.util.AttributeSet;

import com.school.android.models.network.FragmentModel;

public abstract class SpinnerModelObserver<T extends FragmentModel> extends SpinnerObserver<T> {
    public SpinnerModelObserver(Context context) {
        super(context);
    }

    public SpinnerModelObserver(Context context, int mode) {
        super(context, mode);
    }

    public SpinnerModelObserver(Context context, AttributeSet attrs) {
        super(context, attrs);
    }

    public SpinnerModelObserver(Context context, AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }

    public SpinnerModelObserver(Context context, AttributeSet attrs, int defStyleAttr, int mode) {
        super(context, attrs, defStyleAttr, mode);
    }

    public SpinnerModelObserver(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode) {
        super(context, attrs, defStyleAttr, defStyleRes, mode);
    }

    public SpinnerModelObserver(Context context, AttributeSet attrs, int defStyleAttr, int defStyleRes, int mode, Resources.Theme popupTheme) {
        super(context, attrs, defStyleAttr, defStyleRes, mode, popupTheme);
    }

    public int indexOf(T t) {
        for (int i = 0; i < getCount(); i++)
            if (((T) getItemAtPosition(i)).getId() == t.getId())
                return i;
        return -1;
    }

    public void trySetData() {
        if (obj != null) {
            int index = indexOf(obj);
            if (index > -1) {
                setSelection(index);
            }
            obj = null;
        }
    }
}
