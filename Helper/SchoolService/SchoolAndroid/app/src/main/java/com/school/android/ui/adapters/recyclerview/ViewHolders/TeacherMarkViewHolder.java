package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.os.Bundle;
import android.view.View;

import androidx.annotation.NonNull;

import com.school.android.R;

public class TeacherMarkViewHolder extends TeacherClassViewHolder {
    public TeacherMarkViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);
    }

    @Override
    public void onClick() {
        Bundle bundle = new Bundle();
        bundle.putInt(getString(R.string.class_model), object.getId());
        bundle.putInt(getString(R.string.back), R.id.navigation_marks);
        getRealActivity().openFragment(R.id.navigation_marks, bundle);
    }
}
