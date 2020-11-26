package com.example.plantsdictionary.interfaces;

import com.example.plantsdictionary.data.models.actions.Action;
import com.example.plantsdictionary.data.models.FamilyPlant;
import com.example.plantsdictionary.data.models.Favorite;
import com.example.plantsdictionary.data.models.Plants;

import java.util.List;

/**
 * Провайдер данных
 */
public interface DataProvider {
    List<Plants> getAllPlants();

    List<FamilyPlant> getFamilyPlants();

    List<Action> getAllActions();

    boolean isFavoritesExists(int plantId);

    void insertFavorite(Favorite favorite);

    void deleteFavorite(int plantId);

    Plants getPlantById(int id);
}
