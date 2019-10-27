package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.LoginResult;
import com.application.autostation.network.models.output.LoginModel;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;

public interface AuthRetrofit {
    @GET("try")
    Call<ResponseBody> tryConnect(@Header("Authorization") String accessToken);

    @GET("refresh")
    Call<LoginResult> refresh(@Header("token") String accessToken, @Header("refresh") String refreshToken);

    @POST("login")
    Call<LoginResult> login(@Body LoginModel loginModel);

    @POST("register")
    Call<LoginResult> register(@Body LoginModel loginModel);
}
