package com.application.library.ui;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.application.library.app.App;
import com.application.library.network.callbacks.UniversalCallback;
import com.application.library.network.models.input.Book;
import com.school.library.R;

public class BookReadActivity extends AppCompatActivity {

    Book object;

    TextView name;
    TextView count;
    TextView year;
    TextView author;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book_read);

        object = (Book) getIntent().getExtras().getSerializable("book");

        name = findViewById(R.id.name);
        count = findViewById(R.id.count);
        year = findViewById(R.id.year);
        author = findViewById(R.id.author);

        name.setText(object.getName());
        count.setText(String.valueOf(object.getPagesCount()));
        year.setText(String.valueOf(object.getYear()));
        author.setText(object.getAuthor());
    }
}
