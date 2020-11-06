package com.example.plantsdictionary.ui.controls.ui.models;

import com.example.plantsdictionary.data.models.Plants;

public class PlantViewModel {
    private Plants plants;

    public PlantViewModel(Plants plants) {
        this.plants = plants;
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
}
