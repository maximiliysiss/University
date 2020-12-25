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
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.charset.StandardCharsets;

/**
 * Логика для взаимодействия с сервером
 */
public class ClientSocketLogic {

    private final String ip;
    private final int port;

    private Socket socket;

    /**
     * Потоки данных
     */
    private OutputStream out;
    private InputStream in;

    /**
     * Обработчики событий
     */
    private ActionHandler<String> messageHandler = x -> {
    };
    private ActionHandler<String> loadHandler = x -> {
    };
    private ActionHandler<String> failLoginHandler = x -> {
    };
    private ActionHandler<String> onlineHandler = x -> {
    };

    /**
     * Конвертер
     */
    private static Gson gson = new Gson();

    public ClientSocketLogic(String ip, int port) {
        this.ip = ip;
        this.port = port;
    }

    /**
     * Подключение
     */
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

    /**
     * Логин
     *
     * @param login
     * @param password
     */
    public void login(String login, String password) {
        initConnection();
        sendJsonMessage(new ActionMessage("login").packBody(new LoginBody(login, password)));
    }

    /**
     * Регистрация
     *
     * @param login
     * @param password
     */
    public void register(String login, String password) {
        initConnection();
        sendJsonMessage(new ActionMessage("register").packBody(new LoginBody(login, password)));
    }

    /**
     * Обработка входящих сообщений
     */
    private void handleMessages() {
        while (true) {
            try {
                String messageData = readMessageData();

                Messagable messagable = MessageFactory.createMessageFromString(messageData);
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

    /**
     * Получение данных из потока
     *
     * @return
     * @throws IOException
     */
    private String readMessageData() throws IOException {
        byte[] lengthArray = new byte[4];
        in.read(lengthArray, 0, 4);
        ByteBuffer wrapped = ByteBuffer.wrap(lengthArray);
        int length = wrapped.getInt();

        byte[] data = new byte[length];
        in.read(data, 0, length);
        byte[] messageData = Cryptographic.get().decrypt(data);
        return new String(messageData, StandardCharsets.UTF_8);
    }

    /**
     * Обработка действий
     *
     * @param messagable
     */
    private void handleActionMessage(ActionMessage messagable) {
        switch (messagable.getAction()) {
            case "login":
                loginAction(messagable.getBody(LoginResult.class));
                break;
            case "loginfail":
                loginFail(messagable);
                break;
            case "load":
                loadHandler.handle(String.join("\n", messagable.getBody(String[].class)));
                break;
            case "online":
                onlineHandler.handle(String.join("\n", messagable.getBody(String[].class)));
        }
    }

    /**
     * Ошибка входа
     *
     * @param messagable
     */
    private void loginFail(ActionMessage messagable) {
        try {
            failLoginHandler.handle(messagable.getData());
            socket.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * Удачный вход
     *
     * @param body
     */
    private void loginAction(LoginResult body) {
        UserContext.setUser(body.getLogin(), body.getId());
    }

    /**
     * Отправить сообщение серверу
     *
     * @param message
     */
    private void sendMessage(String message) {
        byte[] encrypt = Cryptographic.get().encrypt(message.getBytes(StandardCharsets.UTF_8));
        try {
            ByteBuffer wrapped = ByteBuffer.allocate(4);
            wrapped.putInt(encrypt.length);

            out.write(wrapped.array(), 0, 4);
            out.write(encrypt, 0, encrypt.length);
            out.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * Отправить объект серверу
     *
     * @param obj
     * @param <T>
     */
    public <T> void sendJsonMessage(T obj) {
        sendMessage(gson.toJson(obj));
    }

    /**
     * Регистрация обработчиков
     *
     * @param messageHandler
     */

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

    public void registerOnlineHandler(ActionHandler<String> onlineHandler) {
        synchronized (this.onlineHandler) {
            this.onlineHandler = onlineHandler;
        }
    }
}
