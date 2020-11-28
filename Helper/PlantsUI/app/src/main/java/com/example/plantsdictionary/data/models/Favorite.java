package com.example.plantsdictionary.data.models;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

/**
 * Избранное
 */
@Entity
public class Favorite {

    @PrimaryKey(autoGenerate = true)
    private int id;
    private int plant;

    public Favorite() {
    }

    public Favorite(int plant) {
        this.plant = plant;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getPlant() {
        return plant;
    }

    public void setPlant(int plant) {
        this.plant = plant;
    }
}
