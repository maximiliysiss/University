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

    @GET("")
    Call<List<Children>> getModels();

    @GET("{id}")
    Call<Children> getModel(@Path("id") int id);

    @POST("")
    Call<Children> create(@Body Children t);

    @PUT("{id}")
    Call<Children> update(@Path("id") int id, Children t);

    @DELETE("{id}")
    Call<Children> delete(@Path("id") int id);

    @GET("archived")
    Call<List<Children>> archived();

    @GET("{id}/archive")
    Call<Children> archive(@Path("id") int id);

}
