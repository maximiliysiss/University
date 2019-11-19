package com.application.carrepairandroid.application.UserContext;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.Query;

/**
 * Доступ к таблице пользователя
 */
@Dao
public interface UserDao {

    /**
     * Выбрать только 1 пользователя
     * @return
     */
    @Query("select * from UserContext limit 1")
    UserContext getUser();

    /**
     * Удалить всех пользователей
     */
    @Query("delete from UserContext")
    void delete();

    /**
     * Добавить пользователя
     * @param userContext
     */
    @Insert
    void insert(UserContext userContext);
}
