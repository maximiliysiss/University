package com.example.plantsdictionary.ui.controls.ui.models;

import com.example.plantsdictionary.data.models.Favorite;
import com.example.plantsdictionary.data.models.Plants;
import com.example.plantsdictionary.infrastructure.ioc.IOCFactory;
import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.interfaces.DataProvider;
import com.example.plantsdictionary.ui.fragments.allplants.AllPlantsViewModel;

/**
 * Модель растения
 */
public class PlantViewModel {
    private final AllPlantsViewModel allPlantsViewModel;
    private Plants plants;
    private boolean isFavorite;
    private DataProvider dataProvider = IOCFactory.getIContainer().resolve(DataProvider.class);

    public PlantViewModel(AllPlantsViewModel allPlantsViewModel, Plants plants, boolean isFavorite) {
        this.plants = plants;
        this.isFavorite = isFavorite;
        this.allPlantsViewModel = allPlantsViewModel;
    }

    public String getTitle() {
        return plants.getName();
    }

    public String getDescription() {
        return plants.getDescription();
    }

    public String getFamily() {
        return plants.getFamily();
    }

    public boolean isFavorite() {
        return isFavorite;
    }

    public String getImageName() {
        return plants.getImage();
    }

    /**
     * Выставить фаворит
     * @param favorite
     */
    public void setFavorite(boolean favorite) {
        if (favorite)
            dataProvider.insertFavorite(new Favorite(plants.getId()));
        else
            dataProvider.deleteFavorite(plants.getId());
        isFavorite = favorite;

        allPlantsViewModel.reloadData();
    }

    public int getId() {
        return plants.getId();
    }
}
