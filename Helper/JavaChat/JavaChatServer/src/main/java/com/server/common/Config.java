package com.server.common;

import com.google.gson.Gson;
import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;

/**
 * Конфига (дополнительно смотри в проект клиента)
 */
public class Config {
    private String ip;
    private int port;
    private String key;
    /**
     * Строка подключения
     */
    private String connectionString;
    /**
     * Максимальное количество подключений
     */
    private int maxPool;

    public int getMaxPool() {
        return maxPool;
    }

    public String getConnectionString() {
        return connectionString;
    }

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

        try (InputStream inputStream = ClassLoader.getSystemClassLoader().getResourceAsStream(path)) {
            Gson gson = new Gson();
            return gson.fromJson(IOUtils.toString(inputStream, StandardCharsets.UTF_8), Config.class);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
}
