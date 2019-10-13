package com.example.storage.data;

import androidx.room.Database;
import androidx.room.RoomDatabase;

import com.example.storage.data.daos.GroupDAO;
import com.example.storage.data.daos.GroupWithStoragesDAO;
import com.example.storage.data.daos.StoragePositionDAO;
import com.example.storage.data.models.Group;
import com.example.storage.data.models.StoragePosition;

/**
 * Подключение к БД
 */
@Database(entities = {Group.class, StoragePosition.class}, version = 3)
public abstract class DataContext extends RoomDatabase {

    /**
     * Получение различных DAOs
     * @return
     */
    public abstract StoragePositionDAO getStoragePositionDAO();
    public abstract GroupDAO getGroupDAO();
    public abstract GroupWithStoragesDAO getGroupWithStoragesDAO();

}
