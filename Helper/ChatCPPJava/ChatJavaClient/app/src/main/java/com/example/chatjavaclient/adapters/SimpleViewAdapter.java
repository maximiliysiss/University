package com.example.chatjavaclient.adapters;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.chatjavaclient.adapters.interfaces.BuilderViewHolder;

import java.util.List;

public class SimpleViewAdapter<VH extends AbstractViewHolder<M>, M> extends RecyclerView.Adapter<VH> {

    List<M> models;
    BuilderViewHolder<VH> vhBuilderViewHolder;
    int layoutId;

    public SimpleViewAdapter(List<M> models, BuilderViewHolder<VH> vhBuilderViewHolder, int layoutId) {
        this.models = models;
        this.vhBuilderViewHolder = vhBuilderViewHolder;
        this.layoutId = layoutId;
    }

    @NonNull
    @Override
    public VH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(layoutId, parent, false);
        return vhBuilderViewHolder.build(view);
    }

    @Override
    public void onBindViewHolder(@NonNull VH holder, int position) {
        holder.bind(models.get(position));
    }

    @Override
    public int getItemCount() {
        return models.size();
    }

    public void addItem(M val) {
        models.add(val);
        notifyDataSetChanged();
    }
}
