package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.Point;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface PointRetrofit {
    @GET("points")
    Call<List<Point>> getPoints();

    @POST("points")
    Call<Point> create(@Body Point point);

    @DELETE("points/{id}")
    Call<Point> delete(@Path("id") int id);

    @PUT("points/{id}")
    Call<Point> update(@Path("id") int id, @Body Point point);
}
