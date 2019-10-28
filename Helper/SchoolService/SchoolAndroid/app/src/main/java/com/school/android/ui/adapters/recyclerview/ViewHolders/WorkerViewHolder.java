package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.os.Bundle;
import android.view.View;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.Teacher;
import com.school.android.threadable.Future;

public class WorkerViewHolder extends UserViewHolder {
    public WorkerViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);
    }

    @Override
    public void onClick() {
        Bundle bundle = new Bundle();
        bundle.putBoolean(getString(R.string.is_change), true);
        bundle.putInt(getString(R.string.back), R.id.navigation_workers);
        switch (UserType.values()[object.getUserType()]) {
            case Teacher: {
                Future<Teacher> future = new Future<>(() -> App.getTeacherRetrofit().getModel(object.getId()).execute().body());
                getRealActivity().openFragment(R.id.navigation_users_element, modelName, future.get(), bundle);
                break;
            }
            default:
                getRealActivity().openFragment(R.id.navigation_users_element, modelName, object, bundle);
                break;
        }
    }
}
