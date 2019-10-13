package com.example.storage.data.daos;

import androidx.room.Dao;
import androidx.room.Query;

import com.example.storage.data.models.Group;

import java.util.List;


/**
 * Интерфейс для групп в БД
 */
@Dao
public interface GroupDAO extends DaoCrudTemplate<Group> {

    /**
     * Выбрать все группы
     * @return
     */
    @Query("SELECT * FROM `Group`")
    List<Group> getAllGroups();

}
