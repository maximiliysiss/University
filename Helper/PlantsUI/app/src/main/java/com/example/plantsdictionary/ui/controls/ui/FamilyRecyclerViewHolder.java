package com.example.plantsdictionary.ui.controls.ui;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.ui.controls.base.fragmentmodels.AllPlantsParcelableModel;
import com.example.plantsdictionary.ui.controls.recyclerview.viewholder.CardViewHolder;
import com.example.plantsdictionary.ui.controls.ui.models.FamilyPlantViewModel;
import com.example.plantsdictionary.ui.interfaces.ActivityNavigator;

/**
 * Модель для карточки семейства
 */
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
    public void click() throws IllegalAccessException, InstantiationException {
        super.click();
        /**
         * Надо открыть фрагмент
         */
        ActivityNavigator activityNavigator = (ActivityNavigator) getActivity();
        activityNavigator.navigateTo(R.id.nav_all_plants, R.string.allplantsparcelablemodel, new AllPlantsParcelableModel(false, obj.getTitle()));
    }
}
