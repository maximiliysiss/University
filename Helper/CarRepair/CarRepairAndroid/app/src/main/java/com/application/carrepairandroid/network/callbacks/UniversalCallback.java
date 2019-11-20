package com.application.carrepairandroid.network.callbacks;

import android.content.Context;
import android.widget.Toast;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Обработчик запроса по сети
 * @param <T>
 */
public class UniversalCallback<T> implements Callback<T> {
    CallbackAction<T> callbackAction;

    Context context;

    public UniversalCallback(Context context, CallbackAction<T> callbackAction) {
        this.callbackAction = callbackAction;
        this.context = context;
    }

    @Override
    public void onResponse(Call<T> call, Response<T> response) {
        if (response.body() != null)
            callbackAction.process(response.body());
    }

    @Override
    public void onFailure(Call<T> call, Throwable t) {
        Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
    }
}
