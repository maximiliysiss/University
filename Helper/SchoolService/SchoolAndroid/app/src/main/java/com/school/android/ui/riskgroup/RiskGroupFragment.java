package com.school.android.ui.riskgroup;


import android.os.Bundle;

import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.school.android.R;

/**
 * A simple {@link Fragment} subclass.
 */
public class RiskGroupFragment extends Fragment {


    public RiskGroupFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_risk_group, container, false);
    }

}
