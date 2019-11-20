package com.application.carrepairandroid.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.application.carrepairandroid.R;
import com.application.carrepairandroid.application.App;
import com.application.carrepairandroid.network.callbacks.CallbackAction;
import com.application.carrepairandroid.network.callbacks.UniversalCallback;
import com.application.carrepairandroid.network.models.input.Service;

/**
 * Форма услуги
 */
public class ServiceActivity extends AppCompatActivity {

    /**
     * Услуга
     */
    Service service;

    /**
     * Поля
     */
    EditText name;
    EditText price;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_service);

        /**
         * Получить из intent услуги, с которой работаем
         */
        Bundle bundle = getIntent().getExtras();
        if (bundle != null) {
            service = (Service) bundle.getSerializable(getString(R.string.model_service));
        }

        name = findViewById(R.id.service_name);
        price = findViewById(R.id.price);

        name.setText(service.getName());
        price.setText(String.valueOf(service.getPrice()));

        /**
         * Если для изменения то одно, если для добавления, то другое
         */
        Button action = findViewById(R.id.action);
        if (service.getId() != null) {
            action.setText(R.string.update);
            action.setOnClickListener(v -> {
                loadModel();
                App.getServiceRetrofit().update(service.getId(), service).enqueue(new UniversalCallback<>(getBaseContext(),
                        body -> startActivity(new Intent(ServiceActivity.this, MainActivity.class))));
            });
        } else {
            action.setText(R.string.create);
            Button delete = findViewById(R.id.delete);
            delete.setVisibility(View.INVISIBLE);
            action.setOnClickListener(v -> {
                loadModel();
                App.getServiceRetrofit().create(service).enqueue(new UniversalCallback<>(getBaseContext(),
                        body -> startActivity(new Intent(ServiceActivity.this, MainActivity.class))));
            });
        }

    }

    /**
     * Получим модель с формы
     */
    public void loadModel() {
        service.setName(name.getText().toString().trim());
        service.setPrice(Integer.parseInt(price.getText().toString().trim()));
    }

    /**
     * Удаление
     * @param view
     */
    public void delete(View view) {
        App.getServiceRetrofit().delete(service.getId()).enqueue(new UniversalCallback<>(getBaseContext(),
                body -> startActivity(new Intent(ServiceActivity.this, MainActivity.class))));
    }
}
