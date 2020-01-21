package com.example.testangryandroid.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.os.SystemClock;
import android.view.View;
import android.widget.Button;

import com.example.testangryandroid.R;
import com.example.testangryandroid.ui.extendings.OnSafeClickEvent;

public class MainActivity extends AppCompatActivity {

    Button start;

    @Override
    public void onBackPressed() {
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        start = findViewById(R.id.start);
        start.setOnClickListener(new OnSafeClickEvent(this::startTest));
    }

    public void startTest() {
        startActivity(new Intent(this, PreTestActivity.class));
    }
}
