package com.application.autostation.ui.fragments;

import android.app.Activity;

import androidx.fragment.app.Fragment;

public class ModelContainsFragment<A extends Activity> extends Fragment {

    public A getRealActivity() {
        return (A) getActivity();
    }

}
