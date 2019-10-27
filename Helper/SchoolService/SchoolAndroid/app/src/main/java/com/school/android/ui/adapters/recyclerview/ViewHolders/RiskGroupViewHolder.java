package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.models.network.input.RiskGroup;

public class RiskGroupViewHolder extends RecyclerHolder<RiskGroup> {

    TextView name;

    public RiskGroupViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);
        name = itemView.findViewById(R.id.name);
    }

    @Override
    public void setObject(RiskGroup object) {
        super.setObject(object);
        name.setText(object.getName());
    }

    @Override
    public void onClick() {
        getRealActivity().openFragment(R.id.navigation_riskgroups_element, modelName, object);
    }
}
