package com.school.android.network.classes;

import android.content.Context;
import android.widget.Toast;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class UniversalWithCodeCallback<T> extends BaseResult<T> {

    CallbackActionWithCode<T> callbackAction;

    public UniversalWithCodeCallback(Context context, CallbackActionWithCode<T> callbackAction) {
        super(context);
        this.callbackAction = callbackAction;
    }

    @Override
    public void onResponse(Call<T> call, Response<T> response) {
        super.onResponse(call, response);
        if (isWorked())
            return;

        if (response.body() != null)
            callbackAction.process(response.code(), response.body());
        else
            Toast.makeText(context, "Empty result", Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onFailure(Call<T> call, Throwable t) {
        Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
    }

}
