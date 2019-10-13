package com.example.laboratory_5.dao;

import androidx.lifecycle.LiveData;
import androidx.room.Dao;
import androidx.room.Insert;
import androidx.room.Query;

import com.example.laboratory_5.models.Record;

import java.util.List;

@Dao
public interface RecordDAO {

    @Insert
    void insertAll(Record... records);

    @Query("select * from record")
    List<Record> getRecords();

}
