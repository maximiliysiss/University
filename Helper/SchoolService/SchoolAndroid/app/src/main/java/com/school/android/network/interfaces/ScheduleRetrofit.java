package com.school.android.network.interfaces;

import com.school.android.models.network.input.Schedule;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface ScheduleRetrofit extends CrudRetrofit<Schedule> {

    @GET("{id}/facultative")
    Call<Schedule> toFacultative(@Path("id") int id);

}
