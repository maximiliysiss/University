package com.application.library.app.UserContext;

import androidx.room.Database;
import androidx.room.RoomDatabase;

/**
 * Подключение к БД для хранения пользователя
 */
@Database(entities = {UserContext.class}, version = 1)
public abstract class DbContext extends RoomDatabase {
    /**
     * Получить данные о пользователях
     * @return
     */
    public abstract UserDao userDao();
}
