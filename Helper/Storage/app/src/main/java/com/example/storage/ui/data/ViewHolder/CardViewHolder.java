package com.example.storage.ui.data.ViewHolder;

import android.app.Activity;
import android.content.Context;
import android.content.ContextWrapper;
import android.view.View;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

/**
 * Класс для работы с элементами RecyclerView
 * @param <T>
 */
public abstract class CardViewHolder<T> extends RecyclerView.ViewHolder {


    /**
     * View элемента
     */
    protected View view;
    /**
     * Объект
     */
    protected T obj;

    public void setObj(T obj) {
        this.obj = obj;
    }

    /**
     * Получить Activity с которыми работаем
     * @return
     */
    protected Activity getActivity() {
        Context context = view.getContext();
        while (context instanceof ContextWrapper) {
            if (context instanceof Activity) {
                return (Activity) context;
            }
            context = ((ContextWrapper) context).getBaseContext();
        }
        return null;
    }

    /**
     * Получить ресурс строки
     * @param id
     * @return
     */
    public String getString(int id) {
        return view.getContext().getString(id);
    }

    /**
     *
     * @param itemView
     */
    public CardViewHolder(@NonNull View itemView) {
        super(itemView);
        this.view = itemView;
        this.view.setOnClickListener(v -> click());
    }

    /**
     * Обработка нажатия
     */
    public abstract void click();
}
