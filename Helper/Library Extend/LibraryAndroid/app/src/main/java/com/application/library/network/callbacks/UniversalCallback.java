package com.application.library.network.callbacks;

import android.content.Context;
import android.widget.Toast;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Универсальный обработчик для запросов
 *
 * @param <T>
 */
public class UniversalCallback<T> implements Callback<T> {

    /**
     * Контекст
     */
    Context context;
    /**
     * Обработчик тела ответа
     */
    ActionCallback<T> actionCallback;
    /**
     * Сообщение, если нету тела
     */
    String message;

    public UniversalCallback(Context context, ActionCallback<T> actionCallback) {
        this.context = context;
        this.actionCallback = actionCallback;
    }

    /**
     * Установить сообщение
     *
     * @param message
     * @return
     */
    public UniversalCallback<T> setMessage(String message) {
        this.message = message;
        return this;
    }

    /**
     * Если сервер ответил
     *
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
     * Если была ошибка
     *
     * @param call
     * @param t
     */
    @Override
    public void onFailure(Call<T> call, Throwable t) {
        Toast.makeText(context, t.getMessage(), Toast.LENGTH_SHORT).show();
    }
}
