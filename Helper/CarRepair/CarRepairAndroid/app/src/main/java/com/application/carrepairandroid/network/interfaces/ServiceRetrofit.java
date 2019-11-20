package com.application.carrepairandroid.network.interfaces;

import com.application.carrepairandroid.network.models.input.Service;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

/**
 * Подключение к серверу об услугах
 */
public interface ServiceRetrofit {
    /**
     * Получить услуги
     * @return
     */
    @GET("services")
    Call<List<Service>> getModels();

    /**
     * Добавить услугу
     * @param t
     * @return
     */
    @POST("services")
    Call<Service> create(@Body Service t);

    /**
     * Изменить
     * @param id
     * @param t
     * @return
     */
    @PUT("services/{id}")
    Call<Service> update(@Path("id") int id, @Body Service t);

    /**
     * Удалить
     * @param id
     * @return
     */
    @DELETE("services/{id}")
    Call<Service> delete(@Path("id") int id);

}
