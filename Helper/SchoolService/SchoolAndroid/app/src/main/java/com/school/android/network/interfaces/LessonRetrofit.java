package com.school.android.network.interfaces;

import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.LessonProfile;
import com.school.android.models.network.input.Mark;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface LessonRetrofit {

    @GET("lessons")
    Call<List<Lesson>> getModels();

    @GET("lessons/{id}")
    Call<Lesson> getModel(@Path("id") int id);

    @POST("lessons")
    Call<Lesson> create(@Body Lesson t);

    @PUT("lessons/{id}")
    Call<Lesson> update(@Path("id") int id, @Body Lesson t);

    @DELETE("lessons/{id}")
    Call<Lesson> delete(@Path("id") int id);

    @GET("lessons/{id}/{teacherId}")
    Call<LessonProfile> setLessonProfile(@Path("id") int id, @Path("teacherId") int teacherId);

}
