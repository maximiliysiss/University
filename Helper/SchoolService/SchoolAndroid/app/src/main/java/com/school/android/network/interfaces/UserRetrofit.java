package com.school.android.network.interfaces;

import com.school.android.models.network.input.User;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface UserRetrofit {

    @GET("users")
    Call<List<User>> getModels();

    @GET("users/{id}")
    Call<User> getModel(@Path("id") int id);

    @POST("users")
    Call<User> create(@Body User user);

    @PUT("users/{id}")
    Call<User> update(@Path("id") int id, @Body User user);

    @DELETE("users/{id}")
    Call<User> delete(@Path("id") int id);

    @GET("users/workers")
    Call<List<User>> getWorkers();

}
