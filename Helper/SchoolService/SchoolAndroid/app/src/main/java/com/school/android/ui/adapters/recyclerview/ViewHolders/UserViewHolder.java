package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.Teacher;
import com.school.android.models.network.input.User;
import com.school.android.threadable.Future;

public class UserViewHolder extends RecyclerHolder<User> {

    TextView name;
    TextView surname;
    TextView secondName;
    TextView role;

    public UserViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);

        name = itemView.findViewById(R.id.name);
        surname = itemView.findViewById(R.id.surname);
        secondName = itemView.findViewById(R.id.second_name);
        role = itemView.findViewById(R.id.role);
    }

    @Override
    public void onClick() {
        switch (UserType.values()[object.getUserType()]) {
            case Student: {
                Future<Children> future = new Future<>(() -> App.getChildrenRetrofit().getModel(object.getId()).execute().body());
                getRealActivity().openFragment(R.id.navigation_users_element, modelName, future.get());
                break;
            }
            case Teacher: {
                Future<Teacher> future = new Future<>(() -> App.getTeacherRetrofit().getModel(object.getId()).execute().body());
                getRealActivity().openFragment(R.id.navigation_users_element, modelName, future.get());
                break;
            }
            default:
                getRealActivity().openFragment(R.id.navigation_users_element, modelName, object);
                break;
        }
    }

    @Override
    public void setObject(User object) {
        super.setObject(object);

        name.setText(object.getName());
        surname.setText(object.getSurname());
        secondName.setText(object.getSecondName());
        role.setText(UserType.values()[object.getUserType()].toString());
    }
}
