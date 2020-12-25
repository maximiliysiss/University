package main.java.common;

import com.google.gson.Gson;
import org.apache.commons.io.IOUtils;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;

/**
 * Конфигурация из файла
 */
public class Config {
    private String ip;
    private int port;
    /**
     * Ключ для шифрования
     */
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

    /**
     * Прочитать конфигурацию из файла
     *
     * @param path
     * @return
     */
    public static Config readConfig(String path) {

        try (InputStream inputStream = new FileInputStream(path)) {
            Gson gson = new Gson();
            return gson.fromJson(IOUtils.toString(inputStream, StandardCharsets.UTF_8), Config.class);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
}
