package com.application.flatsandroid.ui.adapters.recyclerview.ViewHolder;

import android.app.Activity;
import android.content.Context;
import android.content.ContextWrapper;
import android.view.View;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

/**
 * Описание элемента для RecyclerView
 *
 * @param <T>
 */
public abstract class RecyclerViewHolder<T> extends RecyclerView.ViewHolder {

    public RecyclerViewHolder(@NonNull View itemView) {
        super(itemView);
        this.itemView.setOnClickListener(x -> onClick());
    }

    /**
     * Объект
     */
    protected T object;

    /**
     * Установить объект
     *
     * @param object
     */
    public void setObject(T object) {
        this.object = object;
    }

    /**
     * Получить форму
     *
     * @return
     */
    protected Activity getActivity() {
        Context context = itemView.getContext();
        while (context instanceof ContextWrapper) {
            if (context instanceof Activity) {
                return (Activity) context;
            }
            context = ((ContextWrapper) context).getBaseContext();
        }
        return null;
    }

    /**
     * Обработка нажатия на элемент
     */
    public abstract void onClick();
}
