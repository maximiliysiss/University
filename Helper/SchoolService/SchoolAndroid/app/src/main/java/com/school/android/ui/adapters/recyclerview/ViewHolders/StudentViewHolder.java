package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.os.Bundle;
import android.view.View;

import androidx.annotation.NonNull;

import com.school.android.R;

public class StudentViewHolder extends UserViewHolder {
    public StudentViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);
    }

    @Override
    public void onClick() {
        super.onClick();

        Bundle bundle = new Bundle();
        bundle.putBoolean(getString(R.string.only_student), true);
        bundle.putInt(getString(R.string.back), R.id.navigation_students);
        getRealActivity().openFragment(R.id.navigation_users_element, modelName, object, bundle);
    }
}
