package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.os.Bundle;
import android.view.View;

import androidx.annotation.NonNull;

import com.school.android.R;

public class TeacherClassViewHolder extends ClassViewHolder {
    public TeacherClassViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);
    }

    @Override
    public void onClick() {
        Bundle bundle = new Bundle();
        bundle.putInt(getString(R.string.mark_model), object.getId());
        getRealActivity().openFragment(R.id.navigation_super_class_element, bundle);
    }
}
