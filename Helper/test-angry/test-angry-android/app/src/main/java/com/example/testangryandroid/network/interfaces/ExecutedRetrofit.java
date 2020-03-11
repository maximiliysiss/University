package com.example.testangryandroid.network.interfaces;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface ExecutedRetrofit {

    /**
     * End test
     * @param result
     * @return
     */
    @POST("executed/{id}")
    Call<ResponseBody> testEnd(@Body Float result, @Path("id")Integer id);

}
