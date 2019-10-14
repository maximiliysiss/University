package com.school.android.network.interfaces;

import com.school.android.models.network.input.ChildInRiskGroup;
import com.school.android.models.network.input.RiskGroup;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface RiskGroupRetrofit extends CrudRetrofit<RiskGroup> {

    @GET("risk/{child}/{group}")
    Call<ChildInRiskGroup> addChildToRiskGroup(@Path("child") int child, @Path("group") int group);

}
