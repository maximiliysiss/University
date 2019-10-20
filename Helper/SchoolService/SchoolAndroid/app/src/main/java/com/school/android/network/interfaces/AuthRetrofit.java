package com.school.android.network.interfaces;

import com.school.android.models.network.input.LoginResult;
import com.school.android.models.network.output.LoginModel;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.Headers;
import retrofit2.http.POST;

public interface AuthRetrofit {
    @POST("login")
    public Call<LoginResult> login(@Body LoginModel loginModel);

    @GET("try")
    public Call<ResponseBody> tryConnect(@Header("Authorization") String token);

    @GET("refresh")
    public Call<LoginResult> refresh(@Header("token") String token, @Header("refresh") String refresh);
}
