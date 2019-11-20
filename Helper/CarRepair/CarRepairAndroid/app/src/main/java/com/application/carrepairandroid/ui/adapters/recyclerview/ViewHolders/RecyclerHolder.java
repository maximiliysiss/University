package com.application.carrepairandroid.ui.adapters.recyclerview.ViewHolders;

import android.app.Activity;
import android.content.Context;
import android.content.ContextWrapper;
import android.view.View;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

/**
 * Обработчик карточки для RecyclerView
 * @param <T>
 */
public abstract class RecyclerHolder<T> extends RecyclerView.ViewHolder {

    public RecyclerHolder(@NonNull View itemView, String modelName) {
        super(itemView);

        this.itemView.setOnClickListener(x -> onClick());
        this.modelName = modelName;
    }

    /**
     * Объект
     */
    protected T object;
    /**
     * Название модели
     */
    protected String modelName;

    public void setObject(T object) {
        this.object = object;
    }

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

    public String getString(int id) {
        return itemView.getContext().getString(id);
    }

    public abstract void onClick();
}
