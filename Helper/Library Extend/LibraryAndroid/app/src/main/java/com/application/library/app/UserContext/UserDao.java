package com.application.library.app.UserContext;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.Query;

/**
 * Взаимодействия для пользовательского контекста
 */
@Dao
public interface UserDao {

    /**
     * Получить пользователя
     * @return
     */
    @Query("select * from UserContext limit 1")
    UserContext getUser();

    /**
     * Удалить пользователя. Храним только 1
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
