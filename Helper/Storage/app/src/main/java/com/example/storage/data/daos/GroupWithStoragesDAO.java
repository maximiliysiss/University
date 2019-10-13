package com.example.storage.data.daos;

import androidx.room.Dao;
import androidx.room.Query;

import com.example.storage.data.models.StorageInGroups;

import java.util.List;

/**
 * Для выборок групп со списком позиций
 */
@Dao
public interface GroupWithStoragesDAO {

    /**
     * Выбрать все группы с позициями
     * @return
     */
    @Query("SELECT * FROM `Group`")
    List<StorageInGroups> getAllGroups();

}
