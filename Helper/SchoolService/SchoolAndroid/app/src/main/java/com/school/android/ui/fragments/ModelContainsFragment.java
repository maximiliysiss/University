package com.school.android.ui.fragments;

import android.app.Activity;

public abstract class ModelContainsFragment<T extends Activity> extends BaseFragment<T> {
    public abstract String getModelName();

    public String getString(String name) {
        String pack = getActivity().getPackageName();
        int resId = getResources().getIdentifier(name, "string", pack);
        return getString(resId);
    }
}
