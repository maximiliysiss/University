package com.school.android.ui.mark;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ExpandableListView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.Mark;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.fragments.ModelContainsFragment;

public class MarkFragment extends ModelContainsFragment<MainActivity> {

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_mark, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        Button add = getView().findViewById(R.id.add);
        add.setOnClickListener(v -> getRealActivity().openFragment(R.id.navigation_mark_element, getModelName(), new Mark()));
        ExpandableListView expandableListView = getView().findViewById(R.id.marks);
        App.getMarkRetrofit().getModels().enqueue(new UniversalCallback<>(getContext(), x -> {

        }));
    }

    @Override
    public String getModelName() {
        return getString(R.string.mark_model);
    }
}