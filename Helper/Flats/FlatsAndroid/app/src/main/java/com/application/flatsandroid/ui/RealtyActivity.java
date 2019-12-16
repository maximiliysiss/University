package com.application.flatsandroid.ui;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.application.flatsandroid.R;
import com.application.flatsandroid.app.App;
import com.application.flatsandroid.network.callbacks.ActionCallback;
import com.application.flatsandroid.network.callbacks.UniversalCallback;
import com.application.flatsandroid.network.models.input.Realty;

public class RealtyActivity extends AppCompatActivity {

    private Realty model;
    EditText name;
    EditText price;
    EditText address;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_realty);
        model = (Realty) getIntent().getExtras().getSerializable("model");


        name = findViewById(R.id.realty);
        address = findViewById(R.id.address);
        price = findViewById(R.id.price);

        name.setText(model.getName());
        address.setText(model.getAddress());
        price.setText(model.getPrice().toString());

        Button action = findViewById(R.id.action);
        Button delete = findViewById(R.id.delete);

        if (model.getId() == 0) {
            delete.setVisibility(View.INVISIBLE);
            action.setText("Добавить");
            action.setOnClickListener(v -> handler(x -> App.getRealtyService().add(x)
                    .enqueue(new UniversalCallback<>(getBaseContext(),
                            y -> startActivity(new Intent(RealtyActivity.this, MainActivity.class))))));
        } else {
            action.setText("Изменить");
            action.setOnClickListener(v -> handler(x -> App.getRealtyService().change(x.getId(), x)
                    .enqueue(new UniversalCallback<>(getBaseContext(),
                            y -> startActivity(new Intent(RealtyActivity.this, MainActivity.class))))));
        }
    }

    public void handler(ActionCallback<Realty> action) {
        if (loadAndValidateModel()) {
            action.process(model);
        } else
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
    }

    public boolean loadAndValidateModel() {
        model.setAddress(address.getText().toString().trim());
        model.setName(name.getText().toString().trim());
        model.setPrice(Double.parseDouble(price.getText().toString().trim()));
        return model.getAddress().length() != 0 && model.getName().length() != 0;
    }

    public void delete(View view) {
        App.getRealtyService().delete(model.getId()).enqueue(new UniversalCallback<>(getBaseContext(),
                x -> startActivity(new Intent(this, MainActivity.class))));
    }
}
