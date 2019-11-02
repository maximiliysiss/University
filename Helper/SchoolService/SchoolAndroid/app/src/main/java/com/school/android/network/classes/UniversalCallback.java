package com.school.android.network.classes;

import android.content.Context;
import android.widget.Toast;

import com.school.android.R;
import com.school.android.ui.activity.ActivityFragmenter;
import com.school.android.ui.fragments.ModelActionFragment;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class UniversalCallback<T> extends BaseResult<T> {

    CallbackAction<T> callbackAction;

    public UniversalCallback(Context context, CallbackAction<T> callbackAction) {
        super(context);
        this.callbackAction = callbackAction;
    }

    public UniversalCallback(Context context, CallbackEmptyAction callbackEmptyAction) {
        super(context);
        this.callbackAction = object -> callbackEmptyAction.action();
    }

    @Override
    public void onResponse(Call<T> call, Response<T> response) {
        super.onResponse(call, response);
        if (isWorked())
            return;

        if (response.body() != null)
            callbackAction.process(response.body());
    }

    @Override
    public void onFailure(Call<T> call, Throwable t) {
        Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
    }
}
