package com.school.android.network.interfaces;

import com.school.android.models.network.input.Children;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface ChildrenRetrofit extends CrudRetrofit<Children> {

    @GET("archived")
    Call<List<Children>> archived();

    @GET("{id}/archive")
    Call<Children> archive(@Path("id") int id);

}
