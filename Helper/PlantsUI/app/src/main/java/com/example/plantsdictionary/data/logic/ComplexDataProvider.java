package com.example.plantsdictionary.data.logic;

import com.example.plantsdictionary.data.models.actions.Action;
import com.example.plantsdictionary.data.models.FamilyPlant;
import com.example.plantsdictionary.data.models.Favorite;
import com.example.plantsdictionary.data.models.Plants;
import com.example.plantsdictionary.infrastructure.threads.Future;
import com.example.plantsdictionary.infrastructure.threads.Job;
import com.example.plantsdictionary.interfaces.DataProvider;

import java.util.List;

/**
 * Составной провайдер данных
 */
public class ComplexDataProvider implements DataProvider {

    /**
     * Провайдер для JSON данных
     */
    private JsonProvider jsonProvider;
    /**
     * Подключение к локальной БД
     */
    private LocalDatabase localDatabase;

    public ComplexDataProvider(JsonProvider jsonProvider, LocalDatabase localDatabase) {
        this.jsonProvider = jsonProvider;
        this.localDatabase = localDatabase;
    }

    /**
     * Получить все растения
     *
     * @return
     */
    @Override
    public List<Plants> getAllPlants() {
        return jsonProvider.getAllPlants();
    }

    /**
     * Получить все семейства
     *
     * @return
     */
    @Override
    public List<FamilyPlant> getFamilyPlants() {
        return jsonProvider.getFamilyPlants();
    }

    /**
     * Получить все действия для первого экрана
     *
     * @return
     */
    @Override
    public List<Action> getAllActions() {
        return jsonProvider.getAllActions();
    }

    /**
     * Проверить является ли растение избранным
     *
     * @param plantId
     * @return
     */
    @Override
    public boolean isFavoritesExists(int plantId) {
        return new Future<>(() -> localDatabase.daoFavorites().isFavoritesExists(plantId) > 0).get();
    }

    /**
     * Добавить новое избранное
     *
     * @param favorite
     */
    @Override
    public void insertFavorite(Favorite favorite) {
        try {
            new Job(x -> localDatabase.daoFavorites().insert(favorite)).join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    /**
     * Удалить избранное
     *
     * @param plantId
     */
    @Override
    public void deleteFavorite(int plantId) {
        try {
            new Job(x -> localDatabase.daoFavorites().delete(plantId)).join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    /**
     * Получить растение по Id
     *
     * @param id
     * @return
     */
    @Override
    public Plants getPlantById(int id) {
        return jsonProvider.getPlantById(id);
    }
}
