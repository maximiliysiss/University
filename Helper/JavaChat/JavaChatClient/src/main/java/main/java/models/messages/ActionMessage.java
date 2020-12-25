package main.java.models.messages;

import com.google.gson.Gson;

/**
 * Действие для сервера
 */
public class ActionMessage implements Messagable {

    private static Gson gson = new Gson();

    /**
     * Название
     */
    private String action;
    /**
     * Данные
     */
    private String data;

    public ActionMessage() {
    }

    public ActionMessage(String action) {
        this.action = action;
    }

    public ActionMessage(String action, String data) {
        this.action = action;
        this.data = data;
    }

    public String getAction() {
        return action;
    }

    public void setAction(String action) {
        this.action = action;
    }

    public String getData() {
        return data;
    }

    public void setData(String data) {
        this.data = data;
    }

    /**
     * Преобразовать Data в класс
     *
     * @param tClass
     * @param <T>
     * @return
     */
    public <T> T getBody(Class<T> tClass) {
        return gson.fromJson(data, tClass);
    }

    /**
     * Упаковать данные в сообщение
     *
     * @param obj
     * @param <T>
     * @return
     */
    public <T> ActionMessage packBody(T obj) {
        data = gson.toJson(obj);
        return this;
    }
}
