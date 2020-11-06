package com.example.plantsdictionary.data.logic;

import android.content.Context;
import android.content.res.XmlResourceParser;

import com.example.plantsdictionary.data.models.Action;
import com.example.plantsdictionary.data.models.FamilyPlant;
import com.example.plantsdictionary.data.models.Plants;
import com.example.plantsdictionary.interfaces.DataProvider;
import com.fasterxml.jackson.dataformat.xml.XmlMapper;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

public class XmlProvider implements DataProvider {

    private Context context;
    private List<Action> actionCache;

    public XmlProvider(Context context) {
        this.context = context;
    }

    @Override
    public List<Plants> getAllPlants() {
        return null;
    }

    @Override
    public List<FamilyPlant> getFamilyPlants() {
        return null;
    }

    @Override
    public List<Action> getAllActions() {
        if (actionCache != null)
            return actionCache;

        try {
            InputStream inputStream = context.getAssets().open("actions.xml");
            XmlMapper xmlMapper = new XmlMapper();
            //actionCache = Arrays.asList(xmlMapper.readValue(inputStreamToString(inputStream), Action[].class));
        } catch (IOException e) {
            e.printStackTrace();
        }

        return actionCache;
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
