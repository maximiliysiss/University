package com.application.autostation.app.UserContext;

import androidx.room.Database;
import androidx.room.RoomDatabase;

/**
 * Доступ к БД
 */
@Database(entities = {UserContext.class}, version = 2)
public abstract class DatabaseContext extends RoomDatabase {
    public abstract UserDao userDao();
}
