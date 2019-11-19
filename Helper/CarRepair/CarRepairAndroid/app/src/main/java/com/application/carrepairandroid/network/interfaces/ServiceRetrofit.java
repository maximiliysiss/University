package com.application.carrepairandroid.network.interfaces;

import com.application.carrepairandroid.network.models.input.Service;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface ServiceRetrofit {
    @GET("services")
    Call<List<Service>> getModels();

    @GET("services/{id}")
    Call<Service> getModel(@Path("id") int id);

    @POST("services")
    Call<Service> create(@Body Service t);

    @PUT("services/{id}")
    Call<Service> update(@Path("id") int id, @Body Service t);

    @DELETE("services/{id}")
    Call<Service> delete(@Path("id") int id);

}
