package com.example.plantsdictionary.data.models;

import java.util.List;

public class FamilyPlant {
    private String name;
    private List<Plants> plants;

    public FamilyPlant(String name, List<Plants> plants) {
        this.name = name;
        this.plants = plants;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Plants> getPlants() {
        return plants;
    }

    public void setPlants(List<Plants> plants) {
        this.plants = plants;
    }
}
