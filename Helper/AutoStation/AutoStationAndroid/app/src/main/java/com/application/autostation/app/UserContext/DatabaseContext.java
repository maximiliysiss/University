package com.application.autostation.app.UserContext;

import androidx.room.Database;
import androidx.room.RoomDatabase;

@Database(entities = {UserContext.class}, version = 1)
public abstract class DatabaseContext extends RoomDatabase {
    public abstract UserDao userDao();
}
