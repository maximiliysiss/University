package com.example.plantsdictionary.interfaces;

import com.example.plantsdictionary.data.models.Action;
import com.example.plantsdictionary.data.models.FamilyPlant;
import com.example.plantsdictionary.data.models.Favorite;
import com.example.plantsdictionary.data.models.Plants;

import java.util.List;

public interface DataProvider {
    List<Plants> getAllPlants();

    List<FamilyPlant> getFamilyPlants();

    List<Action> getAllActions();

    List<Favorite> getAllFavorites();

    void insertFavorite(Favorite favorite);

    void deleteFavorite(Favorite favorite);
}
