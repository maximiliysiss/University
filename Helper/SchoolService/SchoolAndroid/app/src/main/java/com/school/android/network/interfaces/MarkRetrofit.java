package com.school.android.network.interfaces;

import com.school.android.models.network.input.Mark;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface MarkRetrofit {

    @GET("marks")
    Call<List<Mark>> getModels();

    @GET("marks/{id}")
    Call<Mark> getModel(@Path("id") int id);

    @POST("marks")
    Call<Mark> create(@Body Mark t);

    @PUT("marks/{id}")
    Call<Mark> update(@Path("id") int id, @Body Mark t);

    @DELETE("marks/{id}")
    Call<Mark> delete(@Path("id") int id);

    @GET("marks/myclass/{id}")
    Call<List<Mark>> getMyClass(@Path("id") int id, @Query("year") int year, @Query("month") int month, @Query("day") int day);

    @GET("marks/class/{id}")
    Call<List<Mark>> getMarks(@Path("id") int id, @Query("year") int year, @Query("month") int month, @Query("day") int day);

}
