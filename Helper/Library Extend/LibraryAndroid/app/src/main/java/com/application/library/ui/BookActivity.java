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
    EditText count;
    EditText year;
    EditText author;

    Button action;
    Button delete;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book);

        object = (Book) getIntent().getExtras().getSerializable("book");

        name = findViewById(R.id.name);
        count = findViewById(R.id.count);
        year = findViewById(R.id.year);
        author = findViewById(R.id.author);
        delete = findViewById(R.id.delete);
        action = findViewById(R.id.action);

        name.setText(object.getName());
        count.setText(String.valueOf(object.getPagesCount()));
        year.setText(String.valueOf(object.getYear()));
        author.setText(object.getAuthor());

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
        String authorString = author.getText().toString().trim();
        String nameString = name.getText().toString().trim();
        String countString = count.getText().toString().trim();
        String yearString = year.getText().toString().trim();

        if (authorString.length() == 0 || nameString.length() == 0 || countString.length() == 0 || yearString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            return;
        }

        object.setAuthor(authorString);
        object.setName(nameString);
        object.setPagesCount(Integer.parseInt(countString));
        object.setYear(Integer.parseInt(yearString));
    }

    public void delete(View view) {
        App.getBookRetrofit().delete(object.getId()).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            startActivity(new Intent(this, MainActivity.class));
        }));
    }
}
