package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.LoginResult;
import com.application.autostation.network.models.input.User;
import com.application.autostation.network.models.output.ChangeUser;
import com.application.autostation.network.models.output.LoginModel;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.POST;

/**
 * Подключенте к авторизации
 */
public interface AuthRetrofit {
    /**
     * Попытка подключиться
     * @param accessToken
     * @return
     */
    @GET("try")
    Call<ResponseBody> tryConnect(@Header("Authorization") String accessToken);

    /**
     * Попытка обновить токены
     * @param accessToken
     * @param refreshToken
     * @return
     */
    @GET("refresh")
    Call<LoginResult> refresh(@Header("token") String accessToken, @Header("refresh") String refreshToken);

    /**
     * Авторизация
     * @param loginModel
     * @return
     */
    @POST("login")
    Call<LoginResult> login(@Body LoginModel loginModel);
}
