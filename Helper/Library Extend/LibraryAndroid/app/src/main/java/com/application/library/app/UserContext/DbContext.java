package com.application.library.app.UserContext;

import androidx.room.Database;
import androidx.room.RoomDatabase;

@Database(entities = {UserContext.class}, version = 1)
public abstract class DbContext extends RoomDatabase {
    public abstract UserDao userDao();
}
