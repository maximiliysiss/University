package com.example.testangryandroid.network.interfaces;

import com.example.testangryandroid.network.models.ResultModel;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface ExecutedRetrofit {

    @POST("executed")
    Call<ResponseBody> testEnd(@Body ResultModel resultModel);

}
