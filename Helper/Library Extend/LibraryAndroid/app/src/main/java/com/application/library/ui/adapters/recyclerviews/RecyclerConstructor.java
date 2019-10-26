package com.application.library.ui.adapters.recyclerviews;

import android.view.View;

public interface RecyclerConstructor<T> {
    T getView(View view);
}