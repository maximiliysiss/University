package com.school.android.network.interfaces;

import com.school.android.models.network.input.Class;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface ClassRetrofit {

    @GET("")
    Call<List<Class>> getModels();

    @GET("{id}")
    Call<Class> getModel(@Path("id") int id);

    @POST("")
    Call<Class> create(@Body Class t);

    @PUT("{id}")
    Call<Class> update(@Path("id") int id, Class t);

    @DELETE("{id}")
    Call<Class> delete(@Path("id") int id);

}
