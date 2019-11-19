package com.application.carrepairandroid.ui.adapters.recyclerview.ViewHolders;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.application.carrepairandroid.R;
import com.application.carrepairandroid.application.App;
import com.application.carrepairandroid.network.models.input.Service;
import com.application.carrepairandroid.ui.activities.ServiceActivity;

public class ServiceViewHolder extends RecyclerHolder<Service> {

    TextView name;
    TextView price;

    public ServiceViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);
        name = itemView.findViewById(R.id.service_name);
        price = itemView.findViewById(R.id.price);
    }

    @Override
    public void onClick() {
        if (App.getUserContext() != null) {
            Intent intent = new Intent(getActivity(), ServiceActivity.class);
            intent.putExtra(modelName, object);
            getActivity().startActivity(intent);
        }
    }

    @Override
    public void setObject(Service object) {
        super.setObject(object);
        name.setText(object.getName());
        price.setText(String.valueOf(object.getPrice()));
    }
}
