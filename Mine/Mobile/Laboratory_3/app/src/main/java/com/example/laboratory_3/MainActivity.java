package com.example.laboratory_3;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

public class MainActivity extends AppCompatActivity {

    EditText editText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        editText = findViewById(R.id.name);
    }

    public void toSecondActivity(View view) {


        Intent intent = new Intent(this, SecondActivity.class);
        intent.putExtra("name", editText.getText().toString());
        startActivity(intent);
    }
}
