package com.example.plantsdictionary.data.logic;

import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.Query;

import com.example.plantsdictionary.data.models.Favorite;

import java.util.List;

@Dao
public interface DaoFavorites {

    @Query("SELECT * from favorite")
    List<Favorite> getAll();

    @Insert
    void insert(Favorite favorite);

    @Delete
    void delete(Favorite favorite);

}
