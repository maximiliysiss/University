package com.application.library.ui.adapters.recyclerviews;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.application.library.ui.adapters.recyclerviews.ViewHolder.RecyclerViewHolder;

import java.util.List;

/**
 * Адаптер для RecyclerView
 *
 * @param <T>
 * @param <Card>
 */
public class RecyclerViewAdapter<T, Card extends RecyclerViewHolder<T>> extends RecyclerView.Adapter<Card> {

    /**
     * Данные
     */
    List<T> list;
    /**
     * Какого вида карточку использовать
     */
    int layoutItem;
    /**
     * Создатель карточки
     */
    RecyclerConstructor<Card> cardRecyclerViewConstructor;

    public RecyclerViewAdapter(List<T> list, int layoutItem, RecyclerConstructor<Card> cardRecyclerViewConstructor) {
        this.list = list;
        this.layoutItem = layoutItem;
        this.cardRecyclerViewConstructor = cardRecyclerViewConstructor;
    }

    /**
     * Создание карточки
     *
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
     * Привязка данных к карточке
     *
     * @param holder
     * @param position
     */
    @Override
    public void onBindViewHolder(@NonNull Card holder, int position) {
        holder.setObject(list.get(position));
    }

    /**
     * Получить количество
     *
     * @return
     */
    @Override
    public int getItemCount() {
        return list.size();
    }


}
