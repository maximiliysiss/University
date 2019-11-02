package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.User;
import com.application.autostation.network.models.output.ChangeUser;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

/**
 * Изменение пользователя
 */
public interface UserRetrofit {
    /**
     * Изменить пользователя
     * @param changeUser
     * @return
     */
    @POST("auth/userchange")
    Call<ResponseBody> changeUser(@Body ChangeUser changeUser);

    /**
     * Получить пользователя
     * @return
     */
    @GET("auth/currentuser")
    Call<User> getUser();
}
