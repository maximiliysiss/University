package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Children;

public class StudentViewHolder extends RecyclerHolder<Children> {

    TextView name;
    TextView surname;
    TextView secondName;
    TextView role;

    public StudentViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);

        name = itemView.findViewById(R.id.name);
        surname = itemView.findViewById(R.id.surname);
        secondName = itemView.findViewById(R.id.second_name);
        role = itemView.findViewById(R.id.role);
    }

    @Override
    public void onClick() {
        Bundle bundle = new Bundle();
        bundle.putBoolean(getString(R.string.only_student), true);
        bundle.putInt(getString(R.string.back), R.id.navigation_students);
        getRealActivity().openFragment(R.id.navigation_users_element, modelName, object, bundle);
    }

    @Override
    public void setObject(Children object) {
        super.setObject(object);

        name.setText(object.getName());
        surname.setText(object.getSurname());
        secondName.setText(object.getSecondName());
        role.setText(UserType.values()[object.getUserType()].toString());
    }
}
