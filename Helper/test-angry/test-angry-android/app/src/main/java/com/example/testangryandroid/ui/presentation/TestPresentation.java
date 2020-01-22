package com.example.testangryandroid.ui.presentation;

import android.content.Intent;
import android.view.View;

import com.example.testangryandroid.app.App;
import com.example.testangryandroid.network.callbacks.UniversalCallback;
import com.example.testangryandroid.ui.activities.FinalActivity;
import com.example.testangryandroid.ui.activities.TestActivity;

import java.io.IOException;
import java.util.List;

/**
 * Business logic
 */
public class TestPresentation implements Presentational<TestActivity> {

    static TestPresentation testPresentation;

    public static TestPresentation getInstance() {
        if (testPresentation == null)
            testPresentation = new TestPresentation();
        return testPresentation;
    }

    /**
     * All questions
     */
    private List<String> questions;
    private int currentQuestion = 0;
    /**
     * Count of yes
     */
    private int yesAnswers = 0;
    /**
     * Activity
     */
    private TestActivity testActivity;

    private TestPresentation() {
    }

    /**
     * End test
     */
    public void clear() {
        questions = null;
        yesAnswers = currentQuestion = 0;
    }

    private void paint() {
        testActivity.ready.setText(String.valueOf(currentQuestion + 1));
        testActivity.full.setText(String.valueOf(questions.size()));
        testActivity.question.setText(questions.get(currentQuestion));
        testActivity.main.setVisibility(View.VISIBLE);
    }

    @Override
    public void onCreate(TestActivity activity) {
        testActivity = activity;
        if (questions == null)
            App.getQuestionRetrofit().getQuestions().enqueue(new UniversalCallback<>(activity, x -> {
                questions = x;
                paint();
            }));
        else
            paint();
    }

    public void yes() {
        yesAnswers++;
        step();
    }

    /**
     * Get result percent
     * @return
     */
    public float getResult() {
        return (float) yesAnswers / (float) questions.size() * 100;
    }

    private void step() {
        if (questions != null) {
            if (currentQuestion == questions.size() - 1)
                testActivity.startActivity(new Intent(testActivity, FinalActivity.class));
            else {
                currentQuestion++;
                paint();
            }
        }
    }

    public void no() {
        step();
    }
}
