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

public interface MarkRetrofit {

    @GET("")
    Call<List<Mark>> getModels();

    @GET("{id}")
    Call<Mark> getModel(@Path("id") int id);

    @POST("")
    Call<Mark> create(@Body Mark t);

    @PUT("{id}")
    Call<Mark> update(@Path("id") int id, Mark t);

    @DELETE("{id}")
    Call<Mark> delete(@Path("id") int id);

}
