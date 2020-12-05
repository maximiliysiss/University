package main.java.models;

import com.google.gson.Gson;
import main.java.models.messages.ActionMessage;
import main.java.models.messages.Messagable;
import main.java.models.messages.Message;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

public class MessageFactory {

    private static Gson gson = new Gson();

    public static Messagable createMessageFromString(String jsonString) {
        jsonString = jsonString.trim();
        JSONObject jsonValue = (JSONObject) JSONValue.parse(jsonString);
        if (jsonValue.containsKey("action"))
            return gson.fromJson(jsonString, ActionMessage.class);
        return gson.fromJson(jsonString, Message.class);
    }
}
