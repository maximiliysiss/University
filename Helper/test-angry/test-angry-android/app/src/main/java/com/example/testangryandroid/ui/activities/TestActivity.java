package com.example.testangryandroid.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.os.SystemClock;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.example.testangryandroid.R;
import com.example.testangryandroid.ui.extendings.OnSafeClickEvent;
import com.example.testangryandroid.ui.presentation.TestPresentation;

public class TestActivity extends AppCompatActivity {

    public TextView question, ready, full;
    public Button yes, no;
    public LinearLayout main;
    TestPresentation testPresentation;

    @Override
    public void onBackPressed() {
        testPresentation.clear();
        super.onBackPressed();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test);

        main = findViewById(R.id.main);
        main.setVisibility(View.GONE);
        yes = findViewById(R.id.yes);
        no = findViewById(R.id.no);
        question = findViewById(R.id.question);
        ready = findViewById(R.id.ready);
        full = findViewById(R.id.full);

        yes.setOnClickListener(new OnSafeClickEvent(this::yesClick));
        no.setOnClickListener(new OnSafeClickEvent(this::noClick));

        testPresentation = TestPresentation.getInstance();
        testPresentation.onCreate(this);
    }

    public void yesClick() {
        testPresentation.yes();
    }

    public void noClick() {
        testPresentation.no();
    }
}
