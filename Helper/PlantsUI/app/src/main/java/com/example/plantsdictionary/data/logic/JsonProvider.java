package com.example.plantsdictionary.data.logic;

import android.content.Context;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.data.models.actions.Action;
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

/**
 * Проводник для JSON
 */
public class JsonProvider implements DataProvider {

    /**
     * Контекст приложения
     */
    private Context context;

    /**
     * Кэши
     */
    private List<Action> actionCache;
    private List<Plants> plantCache;

    public JsonProvider(Context context) {
        this.context = context;
    }

    @Override
    public List<Plants> getAllPlants() {
        if (plantCache != null)
            return plantCache;

        // Получим файл json, преобразуем к классу
        try {
            InputStream inputStream = context.getAssets().open(context.getString(R.string.datasource_path) + "plants.json");
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
        // Сгруппируем по Family растения и преобразуем к классу
        return plants.stream().collect(Collectors.groupingBy(Plants::getFamily)).entrySet().stream()
                .map(x -> new FamilyPlant(x.getKey(), x.getValue())).collect(Collectors.toList());
    }

    @Override
    public List<Action> getAllActions() {
        if (actionCache != null)
            return actionCache;

        try {
            InputStream inputStream = context.getAssets().open(context.getString(R.string.datasource_path) + "actions.json");
            String content = inputStreamToString(inputStream);
            ObjectMapper objectMapper = new ObjectMapper();
            actionCache = Arrays.asList(objectMapper.readValue(content, Action[].class));
        } catch (IOException e) {
            e.printStackTrace();
        }

        return actionCache;
    }

    @Override
    public boolean isFavoritesExists(int plantId) {
        return false;
    }

    @Override
    public void insertFavorite(Favorite favorite) {
    }

    @Override
    public void deleteFavorite(int plantId) {
    }

    @Override
    public Plants getPlantById(int id) {
        return getAllPlants().stream().filter(x -> x.getId() == id).findFirst().get();
    }

    /**
     * Чтение стрима в строку. HardCode
     *
     * @param is
     * @return
     * @throws IOException
     */
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
