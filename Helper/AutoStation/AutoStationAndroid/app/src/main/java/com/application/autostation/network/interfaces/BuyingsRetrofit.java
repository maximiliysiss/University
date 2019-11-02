package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.Buying;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

/**
 * Покупки
 */
public interface BuyingsRetrofit {

    /**
     * Создать
     * @param buying
     * @return
     */
    @POST("buyings")
    Call<Buying> create(@Body Buying buying);
}
