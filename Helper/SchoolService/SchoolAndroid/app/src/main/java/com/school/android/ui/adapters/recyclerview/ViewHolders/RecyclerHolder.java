package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.app.Activity;
import android.content.Context;
import android.content.ContextWrapper;
import android.view.ContextMenu;
import android.view.View;

import androidx.annotation.NonNull;
import androidx.fragment.app.FragmentActivity;
import androidx.recyclerview.widget.RecyclerView;

import com.school.android.models.network.FragmentModel;
import com.school.android.ui.activity.ActivityFragmenter;

public abstract class RecyclerHolder<T extends FragmentModel> extends RecyclerView.ViewHolder {

    public RecyclerHolder(@NonNull View itemView, String modelName) {
        super(itemView);

        this.itemView.setOnClickListener(x -> onClick());
        this.itemView.setOnLongClickListener(x -> onLongClick());
        this.modelName = modelName;
    }

    private boolean onLongClick() {


        return true;
    }

    protected T object;
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

    protected ActivityFragmenter getRealActivity() {
        return (ActivityFragmenter) getActivity();
    }

    public String getString(int id) {
        return itemView.getContext().getString(id);
    }

    public abstract void onClick();
}
