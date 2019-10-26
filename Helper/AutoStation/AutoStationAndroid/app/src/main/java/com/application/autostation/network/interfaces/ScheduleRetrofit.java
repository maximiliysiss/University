package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.Schedule;

import java.util.List;

import retrofit2.http.GET;

public interface ScheduleRetrofit {
    @GET("shedules")
    List<Schedule> getSchedules();
}
