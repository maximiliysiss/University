package com.example.plantsdictionary.ui.controls.ui.models;

import com.example.plantsdictionary.data.models.FamilyPlant;

/**
 * Модель семейства
 */
public class FamilyPlantViewModel {
    private FamilyPlant familyPlant;

    public FamilyPlantViewModel(FamilyPlant familyPlant) {
        this.familyPlant = familyPlant;
    }

    public String getTitle() {
        return familyPlant.getName();
    }
}
