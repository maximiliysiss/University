package com.school.android.network.interfaces;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface CrudRetrofit<T> {

    @GET("")
    Call<List<T>> getModels();

    @GET("{id}")
    Call<T> getModel(@Path("id") int id);

    @POST("")
    Call<T> create(@Body T t);

    @PUT("{id}")
    Call<T> update(@Path("id") int id, T t);

    @DELETE("{id}")
    Call<T> delete(@Path("id") int id);
}
