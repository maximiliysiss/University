package com.example.plantsdictionary.data.logic;

import com.example.plantsdictionary.data.models.FamilyPlant;
import com.example.plantsdictionary.data.models.Plants;
import com.example.plantsdictionary.interfaces.DataProvider;

import java.util.List;

public class XmlProvider implements DataProvider {
    @Override
    public List<Plants> GetAllPlants() {
        return null;
    }

    @Override
    public List<FamilyPlant> GetFamilyPlants() {
        return null;
    }
}
