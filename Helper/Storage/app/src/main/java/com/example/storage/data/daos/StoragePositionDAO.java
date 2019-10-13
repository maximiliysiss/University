package com.example.storage.data.daos;

import androidx.room.Dao;
import androidx.room.Query;

import com.example.storage.data.models.StoragePosition;

import java.util.List;

/**
 * Для работы с позициями в БД
 */
@Dao
public interface StoragePositionDAO extends DaoCrudTemplate<StoragePosition> {

    @Query("SELECT * FROM StoragePosition")
    List<StoragePosition> getAllPositions();

}
