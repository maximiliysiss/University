package com.application.library.app.UserContext;

import androidx.room.Database;
import androidx.room.Entity;
import androidx.room.RoomDatabase;

@Database(entities = {UserContext.class}, version = 1)
public abstract class DatabaseContext extends RoomDatabase {
    public abstract UserDao userDao();
}
