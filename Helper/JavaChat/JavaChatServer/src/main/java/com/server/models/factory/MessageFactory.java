package com.server.models.factory;

import com.google.gson.Gson;
import com.server.models.ActionMessage;
import com.server.models.Messagable;
import com.server.models.Message;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

public class MessageFactory {

    private static Gson gson = new Gson();

    public static Messagable createMessageFromString(String jsonString) {
        JSONObject jsonValue = (JSONObject) JSONValue.parse(jsonString);
        if (jsonValue.containsKey("action"))
            return gson.fromJson(jsonString, ActionMessage.class);
        return gson.fromJson(jsonString, Message.class);
    }
}
