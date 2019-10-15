package com.school.android.network.classes;

import android.content.Context;
import android.widget.Toast;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class UniversalCallback<T> implements Callback<T> {

    Context context;
    CallbackAction<T> callbackAction;

    public UniversalCallback(Context context, CallbackAction<T> callbackAction) {
        this.context = context;
        this.callbackAction = callbackAction;
    }

    @Override
    public void onResponse(Call<T> call, Response<T> response) {
        callbackAction.process(response.body());
    }

    @Override
    public void onFailure(Call<T> call, Throwable t) {
        Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
    }
}
