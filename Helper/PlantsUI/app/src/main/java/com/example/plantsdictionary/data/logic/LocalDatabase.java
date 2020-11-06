package com.example.plantsdictionary.data.logic;

import androidx.room.Database;
import androidx.room.RoomDatabase;

import com.example.plantsdictionary.data.models.Favorite;

@Database(entities = {Favorite.class}, version = 1)
public abstract class LocalDatabase extends RoomDatabase {
    public abstract DaoFavorites daoFavorites();
}
