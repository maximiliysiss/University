package com.example.plantsdictionary.ui.controls.recyclerview.viewholder;

import android.app.Activity;
import android.content.Context;
import android.content.ContextWrapper;
import android.view.View;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

/**
 * Класс для работы с элементами RecyclerView
 *
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
     *
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
     *
     * @param id
     * @return
     */
    public String getString(int id) {
        return view.getContext().getString(id);
    }

    public int getIdByName(String name) {
        Activity activity = getActivity();
        return activity.getResources().getIdentifier(name, "id", activity.getPackageName());
    }

    public int getStringIdByName(String name) {
        Activity activity = getActivity();
        return activity.getResources().getIdentifier(name, "string", activity.getPackageName());
    }

    public int getDrawableByName(String name) {
        Activity activity = getActivity();
        return activity.getResources().getIdentifier(name, "drawable", activity.getPackageName());
    }

    /**
     * @param itemView
     */
    public CardViewHolder(@NonNull View itemView) {
        super(itemView);
        this.view = itemView;
        this.view.setOnClickListener(v -> {
            try {
                click();
            } catch (InstantiationException | IllegalAccessException e) {
                e.printStackTrace();
            }
        });
    }

    /**
     * Обработка нажатия
     */
    public void click() throws InstantiationException, IllegalAccessException {
    }
}
