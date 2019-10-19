package com.school.android.network.interfaces;

import com.school.android.models.network.input.Teacher;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface TeacherRetrofit {

    @GET("")
    Call<List<Teacher>> getModels();

    @GET("{id}")
    Call<Teacher> getModel(@Path("id") int id);

    @POST("")
    Call<Teacher> create(@Body Teacher t);

    @PUT("{id}")
    Call<Teacher> update(@Path("id") int id, Teacher t);

}
