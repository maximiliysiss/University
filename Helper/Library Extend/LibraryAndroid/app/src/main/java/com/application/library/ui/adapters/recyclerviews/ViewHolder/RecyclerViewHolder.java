package com.application.library.ui.adapters.recyclerviews.ViewHolder;

import android.app.Activity;
import android.content.Context;
import android.content.ContextWrapper;
import android.view.View;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

public abstract class RecyclerViewHolder<T> extends RecyclerView.ViewHolder {

    public RecyclerViewHolder(@NonNull View itemView) {
        super(itemView);

        this.itemView.setOnClickListener(x -> onClick());
        this.itemView.setOnLongClickListener(x -> onLongClick());
    }

    private boolean onLongClick() {


        return true;
    }

    protected T object;

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
