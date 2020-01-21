package com.example.testangryandroid.app;

import android.app.Application;
import android.content.Intent;

import com.example.testangryandroid.R;
import com.example.testangryandroid.network.interfaces.ExecutedRetrofit;
import com.example.testangryandroid.network.interfaces.QuestionRetrofit;
import com.google.gson.Gson;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class App extends Application {

    private static QuestionRetrofit questionRetrofit;
    private static ExecutedRetrofit executedRetrofit;

    private static String userName;

    public static String getUserName() {
        return userName;
    }

    public static void setUserName(String userName) {
        App.userName = userName;
    }

    public static QuestionRetrofit getQuestionRetrofit() {
        return questionRetrofit;
    }

    public static ExecutedRetrofit getExecutedRetrofit() {
        return executedRetrofit;
    }

    @Override
    public void onCreate() {
        super.onCreate();

        Retrofit retrofit = new Retrofit.Builder().baseUrl(getString(R.string.server_url)).addConverterFactory(GsonConverterFactory.create(new Gson())).build();
        questionRetrofit = retrofit.create(QuestionRetrofit.class);
        executedRetrofit = retrofit.create(ExecutedRetrofit.class);
    }
}
