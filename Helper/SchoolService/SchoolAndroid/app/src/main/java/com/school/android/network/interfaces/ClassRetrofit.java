package com.school.android.network.interfaces;

import com.school.android.models.network.input.Class;

import java.util.List;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface ClassRetrofit {

    @GET("classes")
    Call<List<Class>> getModels();

    @GET("classes/{id}")
    Call<Class> getModel(@Path("id") int id);

    @POST("classes")
    Call<Class> create(@Body Class t);

    @PUT("classes/{id}")
    Call<Class> update(@Path("id") int id, @Body Class t);

    @DELETE("classes/{id}")
    Call<Class> delete(@Path("id") int id);

    @GET("classes/teacher/{id}")
    Call<ResponseBody> setTeacher(@Path("id") int id);

}
