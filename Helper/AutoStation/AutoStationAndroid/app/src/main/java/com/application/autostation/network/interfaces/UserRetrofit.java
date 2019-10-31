package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.User;
import com.application.autostation.network.models.output.ChangeUser;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;

public interface UserRetrofit {
    @POST("auth/userchange")
    Call<ResponseBody> changeUser(@Body ChangeUser changeUser);

    @GET("auth/currentuser")
    Call<User> getUser();
}
