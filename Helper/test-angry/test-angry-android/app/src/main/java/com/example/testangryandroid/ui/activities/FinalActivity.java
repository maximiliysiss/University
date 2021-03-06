package com.example.testangryandroid.ui.activities;

import androidx.appcompat.app.AppCompatActivity;
import androidx.constraintlayout.widget.ConstraintLayout;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import com.example.testangryandroid.R;
import com.example.testangryandroid.app.App;
import com.example.testangryandroid.app.UserContext.DatabaseContext;
import com.example.testangryandroid.network.callbacks.UniversalCallback;
import com.example.testangryandroid.network.models.ResultModel;
import com.example.testangryandroid.ui.extendings.OnSafeClickEvent;
import com.example.testangryandroid.ui.presentation.TestPresentation;

/**
 * Final test (last activity)
 */
public class FinalActivity extends AppCompatActivity {

    /**
     * Presentation
     */
    TestPresentation testPresentation;
    /**
     * Main layout
     */
    ConstraintLayout constraintLayout;

    @Override
    public void onBackPressed() {
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_final);
        constraintLayout = findViewById(R.id.main);
        constraintLayout.setOnClickListener(new OnSafeClickEvent(this::clickOnEnd));
        testPresentation = TestPresentation.getInstance();

        TextView textView = findViewById(R.id.finalResult);
        textView.setText(DatabaseContext.getUserContext().getName() + ", ваш результат " + testPresentation.getResult() + "%");
    }

    /**
     * Back to start
     */
    public void clickOnEnd() {
        App.getExecutedRetrofit().testEnd(new ResultModel(DatabaseContext.getUserContext().getName(), testPresentation.getResult())).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            testPresentation.clear();
            startActivity(new Intent(this, MainActivity.class));
        }));
    }
}
