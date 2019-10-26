package com.application.autostation.network.interfaces;

import com.application.autostation.network.models.input.Buying;
import com.application.autostation.network.models.input.Statistics;

import java.util.List;

import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;

public interface BuyingsRetrofit {
    @GET("buyings")
    List<Buying> getBuyings();

    @POST("buyings")
    Buying create(@Body Buying buying);

    @GET("buyings/statistic")
    List<Statistics> getStatistics(@Query("year") int year, @Query("month") int month);
}
