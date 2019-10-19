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

    @GET("")
    Call<List<RiskGroup>> getModels();

    @GET("{id}")
    Call<RiskGroup> getModel(@Path("id") int id);

    @POST("")
    Call<RiskGroup> create(@Body RiskGroup t);

    @PUT("{id}")
    Call<RiskGroup> update(@Path("id") int id, RiskGroup t);

    @DELETE("{id}")
    Call<RiskGroup> delete(@Path("id") int id);

    @GET("risk/{child}/{group}")
    Call<ChildInRiskGroup> addChildToRiskGroup(@Path("child") int child, @Path("group") int group);

}
