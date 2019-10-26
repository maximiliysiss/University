package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.Schedule;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface ScheduleRetrofit {
    @GET("schedules")
    Call<List<Schedule>> getSchedules();

    @POST("schedules")
    Call<Schedule> create(@Body Schedule schedule);

    @DELETE("schedules/{id}")
    Call<Schedule> delete(@Path("id") int id);

    @PUT("schedules/{id}")
    Call<Schedule> update(@Path("id") int id, @Body Schedule schedule);
}
