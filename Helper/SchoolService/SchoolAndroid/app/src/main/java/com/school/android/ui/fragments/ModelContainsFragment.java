package com.school.android.ui.fragments;

import android.app.Activity;
import android.widget.EditText;

import androidx.recyclerview.widget.RecyclerView;

import com.school.android.R;
import com.school.android.models.network.FragmentModel;
import com.school.android.ui.adapters.recyclerview.RecyclerViewAdapterConstructor;
import com.school.android.ui.adapters.recyclerview.ViewHolders.RecyclerHolder;
import com.school.android.ui.callbacks.FilterAction;
import com.school.android.ui.callbacks.TextWatcherCallback;

import java.util.List;

import retrofit2.Call;

public abstract class ModelContainsFragment<T extends Activity> extends BaseFragment<T> {
    public abstract String getModelName();

    public String getString(String name) {
        String pack = getActivity().getPackageName();
        int resId = getResources().getIdentifier(name, "string", pack);
        return getString(resId);
    }

    public <M extends FragmentModel, Card extends RecyclerHolder<M>> void setOnSearchReaction(RecyclerView recyclerView, Call<List<M>> callback, FilterAction<M> filterAction,
                                                                                              RecyclerViewAdapterConstructor<M, Card> cardRecyclerViewAdapter) {
        EditText editText = getActivity().findViewById(R.id.search);
        if (editText != null) {
            editText.addTextChangedListener(new TextWatcherCallback<>(recyclerView, callback, filterAction, getContext(), cardRecyclerViewAdapter));
        }
    }
}
