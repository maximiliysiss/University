package main.java.logic;

import com.google.gson.Gson;
import main.java.common.Cryptographic;
import main.java.common.UserContext;
import main.java.models.MessageFactory;
import main.java.models.messages.ActionMessage;
import main.java.models.messages.Messagable;
import main.java.models.messages.Message;
import main.java.models.messages.actionbody.LoginBody;
import main.java.models.messages.actionbody.LoginResult;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.net.Socket;
import java.nio.charset.StandardCharsets;
import java.util.Scanner;

public class ClientSocketLogic {

    private final String ip;
    private final int port;

    private Socket socket;
    private OutputStream out;
    private InputStream in;

    private ActionHandler<String> messageHandler = x -> {
    };
    private ActionHandler<String> loadHandler = x -> {
    };

    private ActionHandler<String> failLoginHandler = x -> {
    };

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
            in = socket.getInputStream();
            out = socket.getOutputStream();

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
            try {
                int length = in.read();
                byte[] data = new byte[length];
                in.read(data, 0, length);
                byte[] messageData = Cryptographic.get().decrypt(data);

                Messagable messagable = MessageFactory.createMessageFromString(new String(messageData));
                if (messagable instanceof ActionMessage) {
                    handleActionMessage((ActionMessage) messagable);
                    continue;
                }

                Message message = (Message) messagable;
                synchronized (messageHandler) {
                    if (messageHandler != null)
                        messageHandler.handle(message.getSender() + ": " + message.getMessage());
                }
            } catch (IOException e) {
                e.printStackTrace();
                break;
            }
        }
    }

    private void handleActionMessage(ActionMessage messagable) {
        switch (messagable.getAction()) {
            case "login":
                loginAction(messagable.getBody(LoginResult.class));
                break;
            case "loginfail":
                loginFail(messagable);
                break;
            case "reload":
            case "load":
                loadHandler.handle(String.join("\n", messagable.getBody(String[].class)));
                break;
        }
    }

    private void loginFail(ActionMessage messagable) {
        try {
            failLoginHandler.handle(messagable.getData());
            socket.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void loginAction(LoginResult body) {
        UserContext.setUser(body.getLogin(), body.getId());
    }

    public void sendMessage(String message) {
        byte[] encrypt = Cryptographic.get().encrypt(message.getBytes());
        try {
            out.write(encrypt.length);
            out.write(encrypt, 0, encrypt.length);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public <T> void sendJsonMessage(T obj) {
        sendMessage(gson.toJson(obj));
    }

    public void registerMessageHandler(ActionHandler<String> messageHandler) {
        synchronized (this.messageHandler) {
            this.messageHandler = messageHandler;
        }
    }

    public void registerLoadHandler(ActionHandler<String> loadHandler) {
        synchronized (this.loadHandler) {
            this.loadHandler = loadHandler;
        }
    }

    public void registerFailLoginHandler(ActionHandler<String> failLoginHandler) {
        synchronized (this.failLoginHandler) {
            this.failLoginHandler = failLoginHandler;
        }
    }
}
