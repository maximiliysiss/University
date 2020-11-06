package com.example.plantsdictionary.interfaces;

import com.example.plantsdictionary.data.models.FamilyPlant;
import com.example.plantsdictionary.data.models.Plants;

import java.util.List;

public interface DataProvider {
    List<Plants> GetAllPlants();
    List<FamilyPlant> GetFamilyPlants();
}
