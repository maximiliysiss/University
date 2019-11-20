package com.application.carrepairandroid.ui.adapters.recyclerview;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.application.carrepairandroid.ui.adapters.recyclerview.ViewHolders.RecyclerHolder;
import com.application.carrepairandroid.ui.adapters.recyclerview.ViewHolders.RecyclerViewConstructor;

import java.util.List;

/**
 * Адаптер для RecyclerView
 * @param <T>
 * @param <Card>
 */
public class RecyclerViewAdapter<T, Card extends RecyclerHolder<T>> extends RecyclerView.Adapter<Card> {

    /**
     * Данные
     */
    List<T> list;
    /**
     * Layout
     */
    int layoutItem;
    /**
     * Создание карточки
     */
    RecyclerViewConstructor<Card> cardRecyclerViewConstructor;

    public RecyclerViewAdapter(List<T> list, int layoutItem, RecyclerViewConstructor<Card> cardRecyclerViewConstructor) {
        this.list = list;
        this.layoutItem = layoutItem;
        this.cardRecyclerViewConstructor = cardRecyclerViewConstructor;
    }

    /**
     * Создание карточки
     * @param parent
     * @param viewType
     * @return
     */
    @NonNull
    @Override
    public Card onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(layoutItem, parent, false);
        return cardRecyclerViewConstructor.getView(view);
    }

    /**
     * Привязка данных
     * @param holder
     * @param position
     */
    @Override
    public void onBindViewHolder(@NonNull Card holder, int position) {
        holder.setObject(list.get(position));
    }

    @Override
    public int getItemCount() {
        return list.size();
    }


}
