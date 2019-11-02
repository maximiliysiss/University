package com.application.library.ui;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.application.library.R;
import com.application.library.app.App;
import com.application.library.network.callbacks.UniversalCallback;
import com.application.library.network.models.input.Book;

/**
 * Форма для книг
 */
public class BookActivity extends AppCompatActivity {

    /**
     * Книга
     */
    Book object;

    /**
     * Вводы для полей
     */
    EditText name;
    EditText author;
    EditText price;

    /**
     * Кнопки действия
     */
    Button action;
    Button delete;

    /**
     * Создание формы
     *
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book);

        /**
         * Получить книгу, с которой работаем
         */
        object = (Book) getIntent().getExtras().getSerializable("book");

        name = findViewById(R.id.book_name);
        price = findViewById(R.id.price);
        author = findViewById(R.id.author);
        delete = findViewById(R.id.delete);
        action = findViewById(R.id.action);

        name.setText(object.getName());
        author.setText(object.getAuthor());
        price.setText(String.valueOf(object.getPrice()));

        /**
         * Если == 0, то это новая книга
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
     * Загрузим книгу с формы, получим данные с EditView
     *
     * @return
     */
    public boolean loadModel() {
        String nameString = name.getText().toString().trim();
        String authorString = author.getText().toString().trim();
        String priceString = price.getText().toString().trim();

        if (nameString.length() == 0 || authorString.length() == 0 || priceString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            return false;
        }

        object.setName(nameString);
        object.setPrice(Double.parseDouble(priceString));
        object.setAuthor(authorString);
        return true;
    }

    /**
     * Удаление
     *
     * @param view
     */
    public void delete(View view) {
        App.getBookRetrofit().delete(object.getId()).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            startActivity(new Intent(this, MainActivity.class));
        }));
    }
}
