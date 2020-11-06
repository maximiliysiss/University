package com.example.plantsdictionary.ui.controls.ui;

import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.ui.controls.recyclerview.viewholder.CardViewHolder;
import com.example.plantsdictionary.ui.controls.ui.models.FamilyPlantViewModel;
import com.example.plantsdictionary.ui.interfaces.ActivityNavigator;

public class FamilyRecyclerViewHolder extends CardViewHolder<FamilyPlantViewModel> {

    TextView title;
    ImageView image;

    /**
     * @param itemView
     */
    public FamilyRecyclerViewHolder(@NonNull View itemView) {
        super(itemView);

        title = itemView.findViewById(R.id.title);
        image = itemView.findViewById(R.id.image);
    }

    @Override
    public void setObj(FamilyPlantViewModel obj) {
        super.setObj(obj);
        title.setText(obj.getTitle());
    }

    @Override
    public void click() {
        super.click();
        ActivityNavigator activityNavigator = (ActivityNavigator) getActivity();
        Bundle bundle = new Bundle();
        bundle.putString(getString(R.string.family_key), obj.getTitle());
        activityNavigator.navigateTo(R.id.nav_all_plants, bundle);
    }
}
