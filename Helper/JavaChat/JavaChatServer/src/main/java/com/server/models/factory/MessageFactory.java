package com.server.models.factory;

import com.google.gson.Gson;
import com.server.models.ActionMessage;
import com.server.models.Messagable;
import com.server.models.Message;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;
import org.json.simple.parser.ParseException;

/**
 * Фабрика сообщений из String
 * Смотри на клиент
 */
public class MessageFactory {

    private static Gson gson = new Gson();

    public static Messagable createMessageFromString(String jsonString) {
        jsonString = jsonString.trim();
        try {
            JSONObject jsonValue = (JSONObject) JSONValue.parseWithException(jsonString);
            if (jsonValue.containsKey("action"))
                return gson.fromJson(jsonString, ActionMessage.class);
            return gson.fromJson(jsonString, Message.class);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return null;
    }
}
