package com.example.testangryandroid.network.interfaces;

import com.example.testangryandroid.network.models.LoginModel;
import com.example.testangryandroid.network.models.LoginResult;
import com.example.testangryandroid.network.models.RegisterModel;

import okhttp3.RequestBody;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;

public interface AuthRetrofit {

    @POST("auth/login")
    Call<LoginResult> login(@Body LoginModel loginModel);

    @GET("auth/login")
    Call<ResponseBody> tryLogin(@Header("Authorization") String token);

    @POST("auth/register")
    Call<LoginResult> register(@Body RegisterModel registerModel);

    @POST("auth/refreshToken")
    Call<LoginResult> refreshToken(@Header("refreshToken") String refreshToken, @Header("authorization") String authorization);
}
