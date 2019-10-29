package com.school.android.network.classes;

import android.content.Context;
import android.widget.Toast;

import com.school.android.R;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public abstract class BaseResult<T> implements Callback<T> {

    protected Context context;

    public BaseResult(Context context) {
        this.context = context;
    }

    private boolean isWorked = false;

    public boolean isWorked() {
        return isWorked;
    }

    @Override
    public void onResponse(Call<T> call, Response<T> response) {
        if (response.code() == 401 || response.code() == 403) {
            Toast.makeText(context, context.getString(R.string.authorize_error), Toast.LENGTH_SHORT).show();
            isWorked = true;
        } else if (response.code() >= 400) {
            Toast.makeText(context, context.getString(R.string.network_error), Toast.LENGTH_SHORT).show();
            isWorked = true;
        }
    }
}
