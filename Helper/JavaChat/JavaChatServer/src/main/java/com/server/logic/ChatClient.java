package com.server.logic;

import com.google.gson.Gson;
import com.server.common.Cryptographic;
import com.server.data.SqlInteractor;
import com.server.logic.actionbody.LoginBody;
import com.server.logic.actionbody.LoginResult;
import com.server.models.ActionMessage;
import com.server.models.Messagable;
import com.server.models.Message;
import com.server.models.User;
import com.server.models.factory.Actions;
import com.server.models.factory.MessageFactory;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.charset.StandardCharsets;
import java.util.Arrays;
import java.util.List;

/**
 * Представление клиента на сервер
 */
public class ChatClient extends Thread {
    /**
     * Потоки данных
     */
    private final InputStream in;
    private final OutputStream out;

    /**
     * Ссылка на сервер
     */
    private final Serverable serverable;
    /**
     * Ссылка на подключение к БД
     */
    private final SqlInteractor sqlInteractor;

    /**
     * Конвертер JSON
     */
    private final Gson gson = new Gson();

    /**
     * Сокет пользователя
     */
    private final Socket clientSocket;

    /**
     * Пользователь
     */
    private User user;

    /**
     * Работает ли сервак?
     */
    private boolean isRun = true;

    public ChatClient(Socket clientSocket, SqlInteractor sqlInteractor, Serverable serverable) throws IOException {
        this.clientSocket = clientSocket;
        this.serverable = serverable;
        this.sqlInteractor = sqlInteractor;
        this.in = clientSocket.getInputStream();
        this.out = clientSocket.getOutputStream();
    }

    public Integer getUserId() {
        return user.getId();
    }

    public String getUserName() {
        return user.getLogin();
    }

    /**
     * Запуск потока
     */
    @Override
    public void run() {
        super.run();

        this.user = loginUser();
        if (this.user == null) {
            sendJson(Actions.FailLogin);
            serverable.logout(this);
            return;
        }

        sendJson(new ActionMessage("login").packBody(new LoginResult(user.getLogin(), user.getId())));
        loadData();
        broadcastMessage(new Message("Пользователь " + user.getLogin() + " в сети", user.getId(), user.getLogin()));
        serverable.onlineUpdate();

        while (isRun) {
            Messagable messagable = readJsonFromStream();

            if (messagable instanceof ActionMessage) {
                handleAction((ActionMessage) messagable);
                continue;
            }

            Message message = (Message) messagable;

            Integer id = null;
            if (message.getMessage().startsWith("@")) {
                String name = message.getMessage().substring(1, message.getMessage().indexOf(' '));
                String msg = message.getMessage().substring(message.getMessage().indexOf(' ') + 1);
                id = sqlInteractor.getUserByName(name);
                if (id != null) {
                    sqlInteractor.insertPrivateMessage(message, id);
                    message.setMessage("[private] " + msg);
                }
            }

            if (id == null) {
                broadcastMessage(message);
            } else {
                serverable.sendPrivateJsonMessage(message, Arrays.asList(id, getUserId()));
            }

        }
    }

    /**
     * Рассылка сообщений на всех
     *
     * @param message
     */
    private void broadcastMessage(Message message) {
        sqlInteractor.insertMessage(message);
        serverable.sendBroadcastJsonMessage(message);
    }

    /**
     * Обрабокта действий
     *
     * @param message
     */
    private void handleAction(ActionMessage message) {
        switch (message.getAction()) {
            case "clear":
                clearData();
                break;
            case "load":
                loadData();
                break;
            case "logout":
                logout();
                break;
        }
    }

    /**
     * Выход
     */
    private void logout() {
        serverable.logout(this);
        broadcastMessage(new Message("Пользователь " + getUserName() + " покинул чат", getUserId(), getUserName()));
        close();
    }

    /**
     * Отправить историю сообщений
     */
    public void loadData() {
        List<String> messages = sqlInteractor.loadMessages(user.getId());
        sendJson(new ActionMessage("load").packBody(messages));
    }

    /**
     * Очистить историю
     */
    private void clearData() {
        serverable.clearDataAction();
    }

    /**
     * Авторизация
     *
     * @return
     */
    private User loginUser() {
        ActionMessage loginJson = (ActionMessage) readJsonFromStream();
        LoginBody loginBody = loginJson.getBody(LoginBody.class);

        Integer userId = null;
        if (loginJson.getAction().equals("login"))
            userId = sqlInteractor.tryLogin(loginBody.getLogin(), loginBody.getPassword());
        else
            userId = sqlInteractor.tryRegister(loginBody.getLogin(), loginBody.getPassword());

        if (userId == null)
            return null;
        return new User(loginBody.getLogin(), userId);
    }

    /**
     * Получить сообщение из потока
     *
     * @return
     */
    private Messagable readJsonFromStream() {
        try {
            byte[] lengthArray = new byte[4];
            in.read(lengthArray, 0, 4);
            ByteBuffer wrapped = ByteBuffer.wrap(lengthArray);
            int length = wrapped.getInt();

            byte[] data = new byte[length];
            in.read(data, 0, length);
            byte[] messageData = Cryptographic.get().decrypt(data);

            String msg = new String(messageData, StandardCharsets.UTF_8);
            return MessageFactory.createMessageFromString(msg);
        } catch (IOException e) {
        }
        return new ActionMessage("logout");
    }

    /**
     * Отправить данные
     *
     * @param data
     * @param <T>
     */
    public <T> void sendJson(T data) {
        String message = gson.toJson(data);
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
     * Закрыть соединение
     */
    public void close() {
        try {
            this.clientSocket.close();
        } catch (IOException e) {
        }
        this.isRun = false;
    }
}
