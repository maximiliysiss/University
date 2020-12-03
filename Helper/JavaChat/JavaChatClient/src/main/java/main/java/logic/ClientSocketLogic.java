package main.java.logic;

import com.google.gson.Gson;
import main.java.models.MessageFactory;
import main.java.models.messages.ActionMessage;
import main.java.models.messages.Messagable;
import main.java.models.messages.Message;
import main.java.models.messages.actionbody.LoginBody;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;

public class ClientSocketLogic {

    private final String ip;
    private final int port;

    private Socket socket;
    private Scanner in;
    private PrintWriter out;

    private MessageHandler messageHandler;

    private static Gson gson = new Gson();

    public ClientSocketLogic(String ip, int port) {
        this.ip = ip;
        this.port = port;
    }

    private void initConnection() {
        if (socket != null)
            return;

        try {
            socket = new Socket(ip, port);
            in = new Scanner(socket.getInputStream());
            out = new PrintWriter(socket.getOutputStream());

            new Thread(this::handleMessages).start();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void login(String login, String password) {
        initConnection();
        sendJsonMessage(new ActionMessage("login").packBody(new LoginBody(login, password)));
    }

    public void register(String login, String password) {
        initConnection();
        sendJsonMessage(new ActionMessage("register").packBody(new LoginBody(login, password)));
    }

    private void handleMessages() {
        while (true) {
            if (!in.hasNext())
                continue;

            String messageData = in.nextLine();
            Messagable messagable = MessageFactory.createMessageFromString(messageData);
            if (messagable instanceof ActionMessage) {
                handleActionMessage((ActionMessage) messagable);
                continue;
            }

            synchronized (messageHandler) {
                if (messageHandler != null)
                    messageHandler.handle((Message) messageHandler);
            }
        }
    }

    private void handleActionMessage(ActionMessage messagable) {
        switch (messagable.getAction()) {
            case "login":
                break;
            case "loginfail":
                break;
            case "load":
                break;
        }
    }

    public void sendMessage(String message) {
        out.println(message);
    }

    public <T> void sendJsonMessage(T obj) {
        sendMessage(gson.toJson(obj));
    }

    private void registerMessageHandler(MessageHandler messageHandler) {
        synchronized (this.messageHandler) {
            this.messageHandler = messageHandler;
        }
    }
}
