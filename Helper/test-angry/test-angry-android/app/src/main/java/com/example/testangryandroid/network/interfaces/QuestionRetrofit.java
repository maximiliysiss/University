package com.example.testangryandroid.network.interfaces;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface QuestionRetrofit {

    @GET("question")
    Call<List<String>> getQuestions();
}
