package com.example.plantsdictionary.data.logic;

import com.example.plantsdictionary.data.models.Action;
import com.example.plantsdictionary.data.models.FamilyPlant;
import com.example.plantsdictionary.data.models.Favorite;
import com.example.plantsdictionary.data.models.Plants;
import com.example.plantsdictionary.interfaces.DataProvider;

import java.util.List;

public class ComplexProvider implements DataProvider {

    private JsonProvider jsonProvider;
    private LocalDatabase localDatabase;

    public ComplexProvider(JsonProvider jsonProvider, LocalDatabase localDatabase) {
        this.jsonProvider = jsonProvider;
        this.localDatabase = localDatabase;
    }

    @Override
    public List<Plants> getAllPlants() {
        return jsonProvider.getAllPlants();
    }

    @Override
    public List<FamilyPlant> getFamilyPlants() {
        return jsonProvider.getFamilyPlants();
    }

    @Override
    public List<Action> getAllActions() {
        return jsonProvider.getAllActions();
    }

    @Override
    public List<Favorite> getAllFavorites() {
        return localDatabase.daoFavorites().getAll();
    }

    @Override
    public void insertFavorite(Favorite favorite) {
        localDatabase.daoFavorites().insert(favorite);
    }

    @Override
    public void deleteFavorite(Favorite favorite) {
        localDatabase.daoFavorites().delete(favorite);
    }
}
