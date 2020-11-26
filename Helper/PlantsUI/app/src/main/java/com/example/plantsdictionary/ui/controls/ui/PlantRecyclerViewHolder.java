package com.example.plantsdictionary.ui.controls.ui;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.infrastructure.ioc.IOCFactory;
import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.interfaces.ImageProvider;
import com.example.plantsdictionary.ui.controls.base.fragmentmodels.PlantParcelableModel;
import com.example.plantsdictionary.ui.controls.recyclerview.viewholder.CardViewHolder;
import com.example.plantsdictionary.ui.controls.ui.models.PlantViewModel;
import com.example.plantsdictionary.ui.interfaces.ActivityNavigator;

/**
 * Модель для карточки растения
 */
public class PlantRecyclerViewHolder extends CardViewHolder<PlantViewModel> {

    TextView title, family;
    ImageView image, favorite;

    /**
     * @param itemView
     */
    public PlantRecyclerViewHolder(@NonNull View itemView) {
        super(itemView);

        title = itemView.findViewById(R.id.title);
        family = itemView.findViewById(R.id.family);
        image = itemView.findViewById(R.id.image);
        favorite = itemView.findViewById(R.id.favorite);

        favorite.setOnClickListener(v -> obj.setFavorite(!obj.isFavorite()));
    }

    @Override
    public void setObj(PlantViewModel obj) {
        super.setObj(obj);

        title.setText(obj.getTitle());
        family.setText(obj.getFamily());
        image.setImageBitmap(IOCFactory.getIContainer().resolve(ImageProvider.class).loadBitmap(obj.getImageName()));
        favorite.setImageResource(obj.isFavorite() ? R.drawable.ic_favorite_active : R.drawable.ic_favorite);
    }

    @Override
    public void click() throws IllegalAccessException, InstantiationException {
        super.click();
        ActivityNavigator activityNavigator = (ActivityNavigator) getActivity();
        activityNavigator.navigateTo(R.id.nav_plant, R.string.plantparcelablemodel, new PlantParcelableModel(obj.getId()));
    }
}
