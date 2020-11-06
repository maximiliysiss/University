package com.example.plantsdictionary.ui.controls.ui;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.ui.controls.recyclerview.viewholder.CardViewHolder;
import com.example.plantsdictionary.ui.controls.ui.models.PlantViewModel;

public class PlantRecyclerViewHolder extends CardViewHolder<PlantViewModel> {

    TextView title, description, family;
    ImageView image;

    /**
     * @param itemView
     */
    public PlantRecyclerViewHolder(@NonNull View itemView) {
        super(itemView);

        title = itemView.findViewById(R.id.title);
        description = itemView.findViewById(R.id.description);
        family = itemView.findViewById(R.id.family);
        image = itemView.findViewById(R.id.image);
    }

    @Override
    public void setObj(PlantViewModel obj) {
        super.setObj(obj);

        title.setText(obj.getTitle());
        description.setText(obj.getDescription());
        family.setText(obj.getFamily());
    }
}
