package com.application.autostation.network.callbacks;

import android.content.Context;
import android.widget.Toast;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Универсальный обработчик для запроса к серверу
 * @param <T>
 */
public class UniversalCallback<T> implements Callback<T> {

    Context context;
    /**
     * Действие над результатом
     */
    ActionCallback<T> actionCallback;
    /**
     * Сообщение, если нет тела в результате
     */
    String message;

    public UniversalCallback(Context context, ActionCallback<T> actionCallback) {
        this.context = context;
        this.actionCallback = actionCallback;
    }

    public UniversalCallback(Context context, ActionEmptyCallback actionEmptyCallback){
        this.context = context;
        this.actionCallback = t -> actionEmptyCallback.action();
    }

    public UniversalCallback<T> setMessage(String message) {
        this.message = message;
        return this;
    }

    /**
     * Что-то получили от сервера
     * @param call
     * @param response
     */
    @Override
    public void onResponse(Call<T> call, Response<T> response) {
        if (response.body() != null) {
            actionCallback.process(response.body());
        } else {
            if (message != null)
                Toast.makeText(context, message, Toast.LENGTH_SHORT).show();
        }
    }

    /**
     * Ошибочка работа
     * @param call
     * @param t
     */
    @Override
    public void onFailure(Call<T> call, Throwable t) {
        Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
    }
}
