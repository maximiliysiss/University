package com.example.testangryandroid.app.UserContext;

import androidx.room.Database;
import androidx.room.RoomDatabase;

/**
 * Доступ к БД
 */
public  class DatabaseContext {
    private static UserContext userContext;

    public static UserContext getUserContext() {
        return userContext;
    }

    public static void setUserContext(UserContext userContext) {
        DatabaseContext.userContext = userContext;
    }
}
