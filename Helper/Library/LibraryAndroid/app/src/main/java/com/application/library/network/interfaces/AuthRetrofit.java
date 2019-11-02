package com.application.library.network.interfaces;

import com.application.library.network.models.input.LoginResult;
import com.application.library.network.models.output.LoginModel;

import java.util.concurrent.Executor;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;

/**
 * Доступ к авторизации
 */
public interface AuthRetrofit {

    /**
     * Попробовать войти
     * @param accessToken
     * @return
     */
    @GET("try")
    Call<ResponseBody> tryConnect(@Header("Authorization") String accessToken);

    /**
     * Обновить токены
     * @param accessToken
     * @param refreshToken
     * @return
     */
    @GET("refresh")
    Call<LoginResult> refresh(@Header("token") String accessToken, @Header("refresh") String refreshToken);

    /**
     * Логин
     * @param loginModel
     * @return
     */
    @POST("login")
    Call<LoginResult> login(@Body LoginModel loginModel);

    /**
     * Регистрация
     * @param loginModel
     * @return
     */
    @POST("register")
    Call<LoginResult> register(@Body LoginModel loginModel);
}
