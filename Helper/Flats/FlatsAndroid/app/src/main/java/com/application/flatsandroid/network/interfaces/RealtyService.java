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

/**
 * Сервис недвижимости
 */
public interface RealtyService {

    /**
     * Получить все
     * @return
     */
    @GET("realties")
    Call<List<Realty>> getAll();

    /**
     * Добавить
     * @param realty
     * @return
     */
    @POST("realties")
    Call<Realty> add(@Body Realty realty);

    /**
     * Изменить
     * @param id
     * @param realty
     * @return
     */
    @PUT("realties/{id}")
    Call<Realty> change(@Path("id") int id, @Body Realty realty);

    /**
     * Удалить
     * @param id
     * @return
     */
    @DELETE("realties/{id}")
    Call<Realty> delete(@Path("id") int id);

}
