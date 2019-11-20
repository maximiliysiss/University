package com.school.android.ui.callbacks;

import android.content.Context;
import android.text.Editable;
import android.text.TextWatcher;

import androidx.recyclerview.widget.RecyclerView;

import com.school.android.models.network.FragmentModel;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapterConstructor;
import com.school.android.ui.adapters.recyclerview.ViewHolders.RecyclerHolder;

import java.util.List;

import retrofit2.Call;

public class TextWatcherCallback<T extends FragmentModel, Card extends RecyclerHolder<T>> implements TextWatcher {

    RecyclerView recyclerView;
    Call<List<T>> callback;
    FilterAction<T> filterAction;
    Context context;
    RecyclerViewAdapterConstructor<T, Card> viewAdapterConstructor;

    public TextWatcherCallback(RecyclerView recyclerView, Call<List<T>> callback, FilterAction<T> filterAction, Context context, RecyclerViewAdapterConstructor<T, Card> viewAdapterConstructor) {
        this.recyclerView = recyclerView;
        this.callback = callback;
        this.filterAction = filterAction;
        this.context = context;
        this.viewAdapterConstructor = viewAdapterConstructor;
    }

    @Override
    public void beforeTextChanged(CharSequence s, int start, int count, int after) {

    }

    @Override
    public void onTextChanged(CharSequence s, int start, int before, int count) {

    }

    @Override
    public void afterTextChanged(Editable s) {
        String filter = s.toString().trim().toLowerCase();
        callback.clone().enqueue(new UniversalCallback<>(context, object -> {
            if (filter.length() > 0) {
                for (int i = 0; i < object.size(); i++) {
                    if (!filterAction.filter(filter, object.get(i))) {
                        object.remove(object.get(i));
                        i--;
                    }
                }
            }
            recyclerView.setAdapter(viewAdapterConstructor.construct(object));
        }));
    }
}
