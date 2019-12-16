package com.application.flatsandroid.network.interfaces;

import com.application.flatsandroid.network.models.input.LoginResult;
import com.application.flatsandroid.network.models.output.LoginRegisterModel;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface AuthService {

    @POST("login")
    Call<LoginResult> login(@Body LoginRegisterModel loginRegisterModel);

    @POST("register")
    Call<LoginResult> register(@Body LoginRegisterModel loginRegisterModel);

}
