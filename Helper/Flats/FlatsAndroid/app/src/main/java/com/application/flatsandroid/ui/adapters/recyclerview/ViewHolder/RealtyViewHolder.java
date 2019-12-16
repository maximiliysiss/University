package com.application.flatsandroid.ui.adapters.recyclerview.ViewHolder;

import android.content.Intent;
import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.application.flatsandroid.R;
import com.application.flatsandroid.app.App;
import com.application.flatsandroid.network.models.input.Realty;
import com.application.flatsandroid.ui.RealtyActivity;

public class RealtyViewHolder extends RecyclerViewHolder<Realty> {

    TextView name;
    TextView address;
    TextView price;

    public RealtyViewHolder(@NonNull View itemView) {
        super(itemView);

        name = itemView.findViewById(R.id.name);
        address = itemView.findViewById(R.id.address);
        price = itemView.findViewById(R.id.price);
    }

    @Override
    public void onClick() {
        if (App.getRole() == 1)
            return;

        Intent intent = new Intent(this.getActivity(), RealtyActivity.class);
        intent.putExtra("model", object);
        getActivity().startActivity(intent);
    }

    @Override
    public void setObject(Realty object) {
        super.setObject(object);

        name.setText(object.getName());
        address.setText(object.getAddress());
        price.setText(String.valueOf(object.getPrice()));
    }
}
