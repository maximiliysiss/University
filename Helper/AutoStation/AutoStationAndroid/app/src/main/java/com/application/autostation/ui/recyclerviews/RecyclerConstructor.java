package com.application.autostation.ui.recyclerviews;

import android.view.View;

public interface RecyclerConstructor<T> {
    T getView(View view);
}