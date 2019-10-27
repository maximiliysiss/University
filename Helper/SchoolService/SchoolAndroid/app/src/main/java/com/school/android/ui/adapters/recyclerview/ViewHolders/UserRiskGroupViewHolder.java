package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.app.AlertDialog;
import android.view.View;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.RiskGroup;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.threadable.Future;

public class UserRiskGroupViewHolder extends ChildrenViewHolder {

    int riskGroup;

    public UserRiskGroupViewHolder(@NonNull View itemView, String modelName, int riskGroup) {
        super(itemView, modelName);
        this.riskGroup = riskGroup;
    }

    @Override
    public void onClick() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity()).setTitle(getString(R.string.from_riskgroup))
                .setNegativeButton(R.string.no, (dialog, which) -> {
                }).setPositiveButton(R.string.yes, (dialog, which) -> App.getRiskGroupRetrofit().addChildToRiskGroup(object.getId(), riskGroup).enqueue(new UniversalCallback<>(getActivity(), x -> {
                    Future<RiskGroup> future = new Future<>(() -> App.getRiskGroupRetrofit().getModel(riskGroup).execute().body());
                    getRealActivity().openFragment(R.id.navigation_riskgroups_element, getString(R.string.riskgroup_model), future.get());
                })));

        builder.create().show();
    }
}
