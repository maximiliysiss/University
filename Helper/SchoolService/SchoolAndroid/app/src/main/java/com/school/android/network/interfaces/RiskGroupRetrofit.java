package com.school.android.network.interfaces;

import com.school.android.models.network.input.ChildInRiskGroup;
import com.school.android.models.network.input.RiskGroup;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface RiskGroupRetrofit{

    @GET("riskgroups")
    Call<List<RiskGroup>> getModels();

    @GET("riskgroups/{id}")
    Call<RiskGroup> getModel(@Path("id") int id);

    @POST("riskgroups")
    Call<RiskGroup> create(@Body RiskGroup t);

    @PUT("riskgroups/{id}")
    Call<RiskGroup> update(@Path("id") int id, @Body RiskGroup t);

    @DELETE("riskgroups/{id}")
    Call<RiskGroup> delete(@Path("id") int id);

    @GET("riskgroups/risk/{child}/{group}")
    Call<ChildInRiskGroup> addChildToRiskGroup(@Path("child") int child, @Path("group") int group);

}
