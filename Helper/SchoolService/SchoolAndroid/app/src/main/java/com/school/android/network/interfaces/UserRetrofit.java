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

    @GET("")
    Call<List<User>> getModels();

    @GET("{id}")
    Call<User> getModel(@Path("id") int id);

    @POST("")
    Call<User> create(@Body User t);

    @PUT("{id}")
    Call<User> update(@Path("id") int id, User t);

    @DELETE("{id}")
    Call<User> delete(@Path("id") int id);

}
