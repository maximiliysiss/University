package com.school.android.ui.riskgroup;


import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.ChildInRiskGroup;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.RiskGroup;
import com.school.android.network.classes.UniversalWithCodeCallback;
import com.school.android.ui.activity.MainActivity;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.school.android.ui.adapters.recyclerview.ViewHolders.UserRiskGroupViewHolder;
import com.school.android.ui.fragments.ModelActionFragment;

import java.util.List;
import java.util.stream.Collectors;

/**
 * A simple {@link Fragment} subclass.
 */
public class RiskGroupElementFragment extends ModelActionFragment<MainActivity, RiskGroup> {

    EditText name;
    RecyclerView users;

    public RiskGroupElementFragment() {
        super(R.id.navigation_riskgroups);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_risk_group_element, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        name = getView().findViewById(R.id.name);
        users = getView().findViewById(R.id.users);

        name.setText(getModel().getName());
        users.setLayoutManager(new LinearLayoutManager(getContext()));
        if (getModel().getChildInRiskGroups() != null) {
            List<Children> childrenList = getModel().getChildInRiskGroups().stream().map(ChildInRiskGroup::getChild).collect(Collectors.toList());
            users.setAdapter(new RecyclerViewAdapter(childrenList, R.layout.recycler_children, v -> new UserRiskGroupViewHolder(v, getModelName(), getModel().getId())));
        }

        generateModelActions(getView());
    }

    @Override
    public void onSave(RiskGroup riskGroup) {
        App.getRiskGroupRetrofit().update(riskGroup.getId(), riskGroup).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Add, v)));
    }

    @Override
    public void onDelete(int id) {
        App.getRiskGroupRetrofit().delete(id).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Add, v)));
    }

    @Override
    public void onAdd(RiskGroup riskGroup) {
        App.getRiskGroupRetrofit().create(riskGroup).enqueue(new UniversalWithCodeCallback<>(getContext(), (c, v) -> endOperation(c, Operation.Add, v)));
    }

    @Override
    public boolean loadModel() {

        String nameString = name.getText().toString().trim();

        if (nameString.length() == 0)
            return false;

        getModel().setChildInRiskGroups(null);
        getModel().setName(nameString);

        return true;
    }

    @Override
    public String getModelName() {
        return getString(R.string.riskgroup_model);
    }
}
