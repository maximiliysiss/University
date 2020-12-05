package main.java.common;

import com.google.gson.Gson;
import com.google.gson.stream.JsonReader;

import java.io.*;

public class Config {
    private String ip;
    private int port;
    private String key;

    public String getIp() {
        return ip;
    }

    public int getPort() {
        return port;
    }

    public String getKey() {
        return key;
    }

    private Config() {
    }

    public static Config readConfig(String path) {
        try (JsonReader reader = new JsonReader(new FileReader(ClassLoader.getSystemClassLoader().getResource(path).getFile()))) {
            Gson gson = new Gson();
            return gson.fromJson(reader, Config.class);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
}
