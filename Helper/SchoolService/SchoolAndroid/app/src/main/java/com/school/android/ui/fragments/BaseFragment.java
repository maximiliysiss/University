package com.school.android.ui.fragments;

import android.app.Activity;

import androidx.fragment.app.Fragment;

public abstract class BaseFragment<T extends Activity> extends Fragment {
    public BaseFragment() {
    }

    public BaseFragment(int contentLayoutId) {
        super(contentLayoutId);
    }

    public T getRealActivity() {
        return (T) getActivity();
    }
}
