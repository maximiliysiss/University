package com.school.android.ui.adapters.recyclerview;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.school.android.models.network.FragmentModel;
import com.school.android.ui.adapters.recyclerview.ViewHolders.RecyclerHolder;
import com.school.android.ui.adapters.recyclerview.ViewHolders.RecyclerViewConstructor;

import java.util.List;

public class RecyclerViewAdapter<T extends FragmentModel, Card extends RecyclerHolder<T>> extends RecyclerView.Adapter<Card> {

    List<T> list;
    int layoutItem;
    RecyclerViewConstructor<Card> cardRecyclerViewConstructor;

    public RecyclerViewAdapter(List<T> list, int layoutItem, RecyclerViewConstructor<Card> cardRecyclerViewConstructor) {
        this.list = list;
        this.layoutItem = layoutItem;
        this.cardRecyclerViewConstructor = cardRecyclerViewConstructor;
    }

    @NonNull
    @Override
    public Card onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(layoutItem, parent, false);
        return cardRecyclerViewConstructor.getView(view);
    }

    @Override
    public void onBindViewHolder(@NonNull Card holder, int position) {
        holder.setObject(list.get(position));
    }

    @Override
    public int getItemCount() {
        return list.size();
    }


}
