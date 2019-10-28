package com.school.android.network.classes;

import android.content.Context;
import android.widget.Toast;

import com.school.android.R;
import com.school.android.ui.activity.ActivityFragmenter;
import com.school.android.ui.fragments.ModelActionFragment;

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
        if (response.body() != null)
            callbackAction.process(response.body());
        else if (response.code() == 401 || response.code() == 403)
            Toast.makeText(context, context.getString(R.string.authorize_error), Toast.LENGTH_SHORT).show();
        else if (response.code() >= 400 && response.code() < 600)
            Toast.makeText(context, context.getString(R.string.network_error), Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onFailure(Call<T> call, Throwable t) {
        Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
    }
}
