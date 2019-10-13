package com.example.laboratory_5.databaseContext;


import androidx.room.Database;
import androidx.room.RoomDatabase;

import com.example.laboratory_5.dao.RecordDAO;
import com.example.laboratory_5.models.Record;

@Database(entities = {Record.class}, version = 2, exportSchema = false)
public abstract class AppDatabase extends RoomDatabase {
    public  abstract RecordDAO getRecordDao();
}
