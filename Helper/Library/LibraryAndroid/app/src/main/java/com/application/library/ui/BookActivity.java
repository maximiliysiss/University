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

/**
 * Форма книги
 */
public class BookActivity extends AppCompatActivity {

    /**
     * Книга
     */
    Book object;

    /**
     * Поля
     */
    EditText name;
    EditText author;
    EditText count;
    EditText year;

    /**
     * Действия
     */
    Button action;
    Button delete;

    /**
     * Создание книги
     * @param savedInstanceState
     */
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
        author.setText(object.getAuthor());
        count.setText(String.valueOf(object.getPagesCount()));
        year.setText(String.valueOf(object.getYear()));

        /**
         * Если ID == 0, то это новая книга
         */
        if (object.getId() == 0) {
            action.setText("Добавить");
            action.setOnClickListener(v -> {
                if (loadModel())
                    App.getBookRetrofit().add(object).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
                        startActivity(new Intent(BookActivity.this, MainActivity.class));
                    }));
            });
        } else {
            action.setText("Изменить");
            action.setOnClickListener(v -> {
                if (loadModel())
                    App.getBookRetrofit().update(object.getId(), object).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
                        startActivity(new Intent(BookActivity.this, MainActivity.class));
                    }));
            });

            delete.setVisibility(View.VISIBLE);
        }
    }

    /**
     * Получение модели из полей с формы
     * @return
     */
    public boolean loadModel() {
        String nameString = name.getText().toString().trim();
        String authorString = author.getText().toString().trim();
        String countString = count.getText().toString().trim();
        String yearString = year.getText().toString().trim();

        if (nameString.length() == 0 || authorString.length() == 0 || countString.length() == 0 || yearString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            return false;
        }

        object.setName(nameString);
        object.setYear(Integer.parseInt(yearString));
        object.setPagesCount(Integer.parseInt(countString));
        object.setAuthor(authorString);
        return true;
    }

    /**
     * Удалить
     * @param view
     */
    public void delete(View view) {
        App.getBookRetrofit().delete(object.getId()).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            startActivity(new Intent(this, MainActivity.class));
        }));
    }
}
