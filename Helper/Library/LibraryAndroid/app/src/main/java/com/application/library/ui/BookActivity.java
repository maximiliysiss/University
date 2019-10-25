package com.application.library.ui;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.application.library.app.App;
import com.application.library.network.callbacks.UniversalCallback;
import com.application.library.network.models.input.Book;
import com.school.library.R;

public class BookActivity extends AppCompatActivity {

    Book object;

    EditText name;
    EditText price;

    Button action;
    Button delete;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book);

        object = (Book) getIntent().getExtras().getSerializable("book");

        name = findViewById(R.id.name);
        price = findViewById(R.id.price);
        delete = findViewById(R.id.delete);
        action = findViewById(R.id.action);

        name.setText(object.getName());
        price.setText(String.valueOf(object.getPrice()));

        if (object.getId() == 0) {
            action.setText("Добавить");
            action.setOnClickListener(v -> {
                loadModel();
                App.getBookRetrofit().add(object).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
                    startActivity(new Intent(BookActivity.this, MainActivity.class));
                }));
            });
        } else {
            action.setText("Изменить");
            action.setOnClickListener(v -> {
                loadModel();
                App.getBookRetrofit().update(object.getId(), object).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
                    startActivity(new Intent(BookActivity.this, MainActivity.class));
                }));
            });

            delete.setVisibility(View.VISIBLE);
        }
    }

    public void loadModel() {
        String nameString = name.getText().toString().trim();
        String priceString = price.getText().toString().trim();

        if (nameString.length() == 0 || priceString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            return;
        }

        object.setName(nameString);
        object.setPrice(Double.parseDouble(priceString));
    }

    public void delete(View view) {
        App.getBookRetrofit().delete(object.getId()).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            startActivity(new Intent(this, MainActivity.class));
        }));
    }
}
