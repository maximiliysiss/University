package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.models.network.input.Children;

public class ChildrenViewHolder extends RecyclerHolder<Children> {


    TextView name;
    TextView surname;
    TextView secondName;

    public ChildrenViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);

        name = itemView.findViewById(R.id.name);
        surname = itemView.findViewById(R.id.surname);
        secondName = itemView.findViewById(R.id.second_name);
    }

    @Override
    public void onClick() {

    }

    @Override
    public void setObject(Children object) {
        super.setObject(object);

        name.setText(object.getName());
        surname.setText(object.getSurname());
        secondName.setText(object.getSurname());
    }
}
