package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.view.View;

public interface RecyclerViewConstructor<T> {
    T getView(View view);
}
