package com.example.storage.ui.data;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.RecyclerView;

import com.example.storage.ui.data.ViewHolder.CardViewConstructor;
import com.example.storage.ui.data.ViewHolder.CardViewHolder;

import java.util.List;

/**
 * Адаптер для Recycler
 * @param <T>
 * @param <Card>
 */
public class RecyclerCardViewAdapter<T, Card extends CardViewHolder<T>> extends RecyclerView.Adapter<Card> {

    /**
     * Список данных
     */
    private List<T> list;
    /**
     * Фрагмент, в котором рабоатем
     */
    private final Fragment fragment;
    /**
     * Какой layout использовать для эелемента
     */
    private final int itemLayout;
    /**
     * Конструктор элемента
     */
    private final CardViewConstructor<Card> cardViewConstructor;

    public RecyclerCardViewAdapter(List<T> list, Fragment fragment, int itemLayout, CardViewConstructor<Card> cardViewConstructor) {
        this.list = list;
        this.fragment = fragment;
        this.itemLayout = itemLayout;
        this.cardViewConstructor = cardViewConstructor;
    }

    /**
     * Создание нового элемента
     * @param parent
     * @param viewType
     * @return
     */
    @NonNull
    @Override
    public Card onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(parent.getContext()).inflate(itemLayout, parent, false);
        Card pvh = cardViewConstructor.getCard(v);
        return pvh;
    }

    /**
     * Заполнение нового элемента
     * @param holder
     * @param position
     */
    @Override
    public void onBindViewHolder(@NonNull Card holder, int position) {
        holder.setObj(list.get(position));
    }

    /**
     * Получить количество
     * @return
     */
    @Override
    public int getItemCount() {
        return list.size();
    }
}
