package com.example.plantsdictionary.data.logic;

import androidx.room.Database;
import androidx.room.RoomDatabase;

import com.example.plantsdictionary.data.models.Favorite;

/**
 * База данных
 */
@Database(entities = {Favorite.class}, version = 2)
public abstract class LocalDatabase extends RoomDatabase {
    public abstract DaoFavorites daoFavorites();
}
