package com.application.library.ui;

import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.application.library.app.App;
import com.application.library.network.callbacks.UniversalCallback;
import com.application.library.network.models.addons.UserRole;
import com.application.library.network.models.input.Book;
import com.application.library.ui.adapters.recyclerviews.RecyclerViewAdapter;
import com.application.library.ui.adapters.recyclerviews.ViewHolder.BookViewHolder;
import com.school.library.R;

/**
 * Форма списка книг
 */
public class MainActivity extends AppCompatActivity {

    /**
     * Вывод списка книг
     */
    RecyclerView recyclerView;
    /**
     * Кнопка добавить
     */
    Button add;

    /**
     * При нажатии назад - Сменить пользователя?
     */
    @Override
    public void onBackPressed() {
        AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this);
        builder.setTitle("Сменить пользователя?")
                .setPositiveButton("Да", (dialog, which) -> {
                    App.signOut();
                    startActivity(new Intent(this, LoginActivity.class));
                })
                .setNegativeButton("Нет", (dialog, which) -> {
                });
        builder.create().show();
    }

    /**
     * Создание формы
     *
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        add = findViewById(R.id.add);
        recyclerView = findViewById(R.id.books);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        /**
         * Загрузим книги и выведем
         */
        App.getBookRetrofit().getBooks().enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_book, y -> new BookViewHolder(y)));
        }));

        /**
         * Если не админ, то скроем кнопку добавить
         */
        if (UserRole.values()[App.getUserContext().getUserRole()] != UserRole.Admin)
            add.setVisibility(View.INVISIBLE);

    }

    /**
     * Кнопка добавить
     *
     * @param view
     */
    public void add(View view) {
        Intent intent = new Intent(this, BookActivity.class);
        intent.putExtra("book", new Book());
        startActivity(intent);
    }
}
