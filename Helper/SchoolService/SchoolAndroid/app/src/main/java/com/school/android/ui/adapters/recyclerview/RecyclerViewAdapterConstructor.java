package com.school.android.ui.adapters.recyclerview;

import com.school.android.models.network.FragmentModel;
import com.school.android.ui.adapters.recyclerview.ViewHolders.RecyclerHolder;
import com.school.android.ui.adapters.recyclerview.ViewHolders.RecyclerViewConstructor;

import java.util.List;

public class RecyclerViewAdapterConstructor<T extends FragmentModel, Card extends RecyclerHolder<T>> {

    int layout;
    RecyclerViewConstructor<Card> cardRecyclerViewConstructor;

    public RecyclerViewAdapterConstructor(int layout, RecyclerViewConstructor<Card> cardRecyclerViewConstructor) {
        this.layout = layout;
        this.cardRecyclerViewConstructor = cardRecyclerViewConstructor;
    }

    public RecyclerViewAdapter<T, Card> construct(List<T> data) {
        return new RecyclerViewAdapter<T, Card>(data, layout, cardRecyclerViewConstructor);
    }
}
