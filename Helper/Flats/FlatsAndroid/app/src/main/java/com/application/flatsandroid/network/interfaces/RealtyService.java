package com.application.flatsandroid.network.interfaces;

import com.application.flatsandroid.network.models.input.Realty;

import java.util.List;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface RealtyService {

    @GET("realties")
    Call<List<Realty>> getAll();

    @POST("realties")
    Call<Realty> add(@Body Realty realty);

    @PUT("realties/{id}")
    Call<Realty> change(@Path("id") int id, @Body Realty realty);

    @DELETE("realties/{id}")
    Call<Realty> delete(@Path("id") int id);

}
