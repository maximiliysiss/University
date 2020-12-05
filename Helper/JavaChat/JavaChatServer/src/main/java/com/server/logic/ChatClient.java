package com.server.logic;

import com.google.gson.Gson;
import com.server.common.Cryptographic;
import com.server.data.SqlInteractor;
import com.server.logic.actionbody.LoginBody;
import com.server.logic.actionbody.LoginResult;
import com.server.models.*;
import com.server.models.factory.Actions;
import com.server.models.factory.MessageFactory;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;

public class ChatClient extends Thread {
    private final Scanner in;
    private final PrintWriter out;

    private final Serverable serverable;
    private final SqlInteractor sqlInteractor;

    private final Gson gson = new Gson();
    private final Socket clientSocket;
    private User user;

    public ChatClient(Socket clientSocket, SqlInteractor sqlInteractor, Serverable serverable) throws IOException {
        this.clientSocket = clientSocket;
        this.serverable = serverable;
        this.sqlInteractor = sqlInteractor;
        this.in = new Scanner(clientSocket.getInputStream());
        this.out = new PrintWriter(clientSocket.getOutputStream());
    }

    public Integer getUserId(){
        return user.getId();
    }

    @Override
    public void run() {
        super.run();

        this.user = loginUser();
        if (this.user == null) {
            sendJson(Actions.FailLogin);
            return;
        }

        sendJson(new ActionMessage("login").packBody(new LoginResult(user.getLogin(), user.getId())));

        while (true) {
            if (!in.hasNext())
                continue;

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
                message.setMessage(msg);
                id = sqlInteractor.getUserByName(name);
            }

            if (id == null){
                sqlInteractor.insertMessage(message);
                serverable.sendBroadcastJsonMessage(message);
            }
            else {
                sqlInteractor.insertPrivateMessage(message, user.getId());
                serverable.sendPrivateJsonMessage(message, id);
            }

        }
    }

    private void handleAction(ActionMessage message) {
        switch (message.getAction()) {
            case "clear":
                clearData();
                break;
            case "load":
                loadData();
                break;
        }
    }

    private void loadData() {
        String[] messages = sqlInteractor.loadMessages(user.getId());
        sendJson(new ActionMessage("load").packBody(messages));
    }

    private void clearData() {
        sqlInteractor.clearData();
        sendJson(new ActionMessage("reload"));
    }

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

    private Messagable readJsonFromStream() {
        String inputMessage = in.nextLine();
        String msg = Cryptographic.get().decrypt(inputMessage);
        return MessageFactory.createMessageFromString(msg);
    }

    public <T> void sendJson(T data) {
        String message = gson.toJson(data);
        String crypt = Cryptographic.get().encrypt(message);
        out.println(crypt);
    }

    public void close() {
        try {
            this.clientSocket.close();
        } catch (IOException e) {
        }
    }
}
