package com.example.plantsdictionary.data.logic;

import android.content.Context;
import android.os.Build;

import androidx.annotation.RequiresApi;

import com.example.plantsdictionary.data.models.Action;
import com.example.plantsdictionary.data.models.FamilyPlant;
import com.example.plantsdictionary.data.models.Favorite;
import com.example.plantsdictionary.data.models.Plants;
import com.example.plantsdictionary.interfaces.DataProvider;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

public class JsonProvider implements DataProvider {

    private Context context;
    private List<Action> actionCache;
    private List<Plants> plantCache;

    public JsonProvider(Context context) {
        this.context = context;
    }

    @Override
    public List<Plants> getAllPlants() {
        if (plantCache != null)
            return plantCache;

        try {
            InputStream inputStream = context.getAssets().open("datasource/plants.json");
            String content = inputStreamToString(inputStream);
            ObjectMapper objectMapper = new ObjectMapper();
            plantCache = Arrays.asList(objectMapper.readValue(content, Plants[].class));
        } catch (IOException e) {
            e.printStackTrace();
        }

        return plantCache;
    }

    @Override
    public List<FamilyPlant> getFamilyPlants() {
        List<Plants> plants = getAllPlants();
        return plants.stream().collect(Collectors.groupingBy(Plants::getFamily)).entrySet().stream()
                .map(x -> new FamilyPlant(x.getKey(), x.getValue())).collect(Collectors.toList());
    }

    @Override
    public List<Action> getAllActions() {
        if (actionCache != null)
            return actionCache;

        try {
            InputStream inputStream = context.getAssets().open("datasource/actions.json");
            String content = inputStreamToString(inputStream);
            ObjectMapper objectMapper = new ObjectMapper();
            actionCache = Arrays.asList(objectMapper.readValue(content, Action[].class));
        } catch (IOException e) {
            e.printStackTrace();
        }

        return actionCache;
    }

    @Override
    public List<Favorite> getAllFavorites() {
        return null;
    }

    @Override
    public void insertFavorite(Favorite favorite) {

    }

    @Override
    public void deleteFavorite(Favorite favorite) {

    }

    private String inputStreamToString(InputStream is) throws IOException {
        StringBuilder sb = new StringBuilder();
        String line;
        BufferedReader br = new BufferedReader(new InputStreamReader(is));
        while ((line = br.readLine()) != null) {
            sb.append(line);
        }
        br.close();
        return sb.toString();
    }
}
