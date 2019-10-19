package com.school.android.network.interfaces;

import com.school.android.models.network.input.Schedule;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface ScheduleRetrofit {

    @GET("{id}/facultative")
    Call<Schedule> toFacultative(@Path("id") int id);

    @GET("")
    Call<List<Schedule>> getModels();

    @GET("{id}")
    Call<Schedule> getModel(@Path("id") int id);

    @POST("")
    Call<Schedule> create(@Body Schedule t);

    @PUT("{id}")
    Call<Schedule> update(@Path("id") int id, Schedule t);

    @DELETE("{id}")
    Call<Schedule> delete(@Path("id") int id);

}
