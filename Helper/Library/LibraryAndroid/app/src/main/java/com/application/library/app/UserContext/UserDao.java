package com.application.library.app.UserContext;

import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.Query;

@Dao
public interface UserDao {

    @Query("select * from UserContext limit 1")
    UserContext getUser();

    @Query("delete from UserContext")
    void delete();

    @Insert
    void insert(UserContext userContext);
}
