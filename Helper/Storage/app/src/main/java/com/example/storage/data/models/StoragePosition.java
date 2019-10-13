package com.example.storage.data.models;

import androidx.room.ColumnInfo;
import androidx.room.Entity;
import androidx.room.ForeignKey;
import androidx.room.Ignore;
import androidx.room.PrimaryKey;
import androidx.room.TypeConverters;

import com.example.storage.data.converters.DateConverter;
import com.example.storage.utils.DateUtils;

import java.io.Serializable;
import java.util.Calendar;
import java.util.Date;

/**
 * Позиция покупки
 */
@Entity
@ForeignKey(entity = Group.class, parentColumns = "id", childColumns = "groupId", onDelete = ForeignKey.CASCADE)
public class StoragePosition implements Serializable {
    @PrimaryKey(autoGenerate = true)
    private int id;
    private double sum;
    @ColumnInfo(name = "groupId")
    private int groupId;
    @TypeConverters(DateConverter.class)
    private Date date;

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public double getSum() {
        return sum;
    }

    public void setSum(double sum) {
        this.sum = sum;
    }

    public int getGroupId() {
        return groupId;
    }

    public void setGroupId(int groupId) {
        this.groupId = groupId;
    }

    @Ignore
    public StoragePosition(int id, double price, int groupId) {
        this.id = id;
        this.sum = price;
        this.groupId = groupId;
    }

    public StoragePosition() {
        date = DateUtils.today();
    }
}
