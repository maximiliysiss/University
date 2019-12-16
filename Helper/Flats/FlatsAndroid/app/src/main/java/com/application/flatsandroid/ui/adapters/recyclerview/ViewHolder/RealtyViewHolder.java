package com.application.flatsandroid.ui.adapters.recyclerview.ViewHolder;

import android.content.Intent;
import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.application.flatsandroid.R;
import com.application.flatsandroid.app.App;
import com.application.flatsandroid.network.models.input.Realty;
import com.application.flatsandroid.ui.RealtyActivity;

/**
 * Обработчик для карточки для списка
 */
public class RealtyViewHolder extends RecyclerViewHolder<Realty> {

    /**
     * Поля
     */
    TextView name;
    TextView address;
    TextView price;

    public RealtyViewHolder(@NonNull View itemView) {
        super(itemView);

        name = itemView.findViewById(R.id.name);
        address = itemView.findViewById(R.id.address);
        price = itemView.findViewById(R.id.price);
    }

    /**
     * При клике
     */
    @Override
    public void onClick() {
        if (App.getRole() == 1)
            return;

        Intent intent = new Intent(this.getActivity(), RealtyActivity.class);
        intent.putExtra("model", object);
        getActivity().startActivity(intent);
    }

    /**
     * Заполнение объектом
     * @param object
     */
    @Override
    public void setObject(Realty object) {
        super.setObject(object);

        name.setText(object.getName());
        address.setText(object.getAddress());
        price.setText(String.valueOf(object.getPrice()));
    }
}
