package com.example.plantsdictionary.data.logic;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.Query;

import com.example.plantsdictionary.data.models.Favorite;

/**
 * Объект доступа к избранному
 */
@Dao
public interface DaoFavorites {

    /**
     * Проверить количество фаворитов для plantId
     *
     * @param plantId
     * @return
     */
    @Query("SELECT Count(*) from favorite where Plant = :plantId")
    int isFavoritesExists(int plantId);

    /**
     * Добавить избранное
     *
     * @param favorite
     */
    @Insert
    void insert(Favorite favorite);

    /**
     * Удалить избранное по plantId
     *
     * @param plantId
     */
    @Query("DELETE from favorite where Plant = :plantId")
    void delete(int plantId);

}
