package com.application.carrepairandroid.network.interfaces;

import com.application.carrepairandroid.network.models.input.LoginResult;
import com.application.carrepairandroid.network.models.output.LoginModel;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;

/**
 * Запрос к авторизации
 */
public interface AuthRetrofit {
    /**
     * Вход
     * @param loginModel
     * @return
     */
    @POST("login")
    public Call<LoginResult> login(@Body LoginModel loginModel);

    /**
     * Проверка соединения
     * @param token
     * @return
     */
    @GET("try")
    public Call<ResponseBody> tryConnect(@Header("Authorization") String token);

    /**
     * Обновление токена
     * @param token
     * @param refresh
     * @return
     */
    @GET("refresh")
    public Call<LoginResult> refresh(@Header("token") String token, @Header("refresh") String refresh);
}
