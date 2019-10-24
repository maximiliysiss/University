package com.school.android.network.interfaces;

import com.school.android.models.network.input.Children;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface ChildrenRetrofit {

    @GET("children")
    Call<List<Children>> getModels();

    @GET("children/{id}")
    Call<Children> getModel(@Path("id") int id);

    @POST("children")
    Call<Children> create(@Body Children t);

    @PUT("children/{id}")
    Call<Children> update(@Path("id") int id, @Body Children t);

    @DELETE("children/{id}")
    Call<Children> delete(@Path("id") int id);

    @GET("children/archived")
    Call<List<Children>> archived();

    @GET("children/{id}/archive")
    Call<Children> archive(@Path("id") int id);

}
