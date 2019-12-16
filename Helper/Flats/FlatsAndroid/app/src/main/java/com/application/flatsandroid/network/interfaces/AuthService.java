package com.application.flatsandroid.network.interfaces;

import com.application.flatsandroid.network.models.input.LoginResult;
import com.application.flatsandroid.network.models.output.LoginRegisterModel;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

/**
 * Сервис авторизации
 */
public interface AuthService {

    /**
     * Логин
     * @param loginRegisterModel
     * @return
     */
    @POST("login")
    Call<LoginResult> login(@Body LoginRegisterModel loginRegisterModel);

    /**
     * Регистрация
     * @param loginRegisterModel
     * @return
     */
    @POST("register")
    Call<LoginResult> register(@Body LoginRegisterModel loginRegisterModel);

}
