package com.server.logic;

import com.google.gson.Gson;
import com.server.data.SqlInteractor;
import com.server.logic.actionbody.LoginBody;
import com.server.logic.actionbody.LoginResult;
import com.server.models.*;

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
    private User user;

    public ChatClient(Socket clientSocket, SqlInteractor sqlInteractor, Serverable serverable) throws IOException {
        this.serverable = serverable;
        this.sqlInteractor = sqlInteractor;
        this.in = new Scanner(clientSocket.getInputStream());
        this.out = new PrintWriter(clientSocket.getOutputStream());
    }

    @Override
    public void run() {
        super.run();

        this.user = loginUser();
        if (this.user == null) {
            sendJson(new ActionMessage("loginfail", "User not found / This user is registered yet"));
            return;
        }

        sendJson(new ActionMessage("login").packBody(new LoginResult(user.getLogin(), user.getId())));

        while (true) {
            if (!in.hasNext())
                continue;

            Message message = readJsonFromStream(Message.class);

            Integer id = null;

            if (message.getMessage().startsWith("@")) {
                String name = message.getMessage().substring(1, message.getMessage().indexOf(' '));
                String msg = message.getMessage().substring(message.getMessage().indexOf(' ') + 1);
                message.setMessage(msg);
                id = sqlInteractor.getUserByName(name);
            }

            if (id == null)
                sqlInteractor.insertMessage(message);
            else
                sqlInteractor.insertPrivateMessage(message, user.getId());

            serverable.sendBroadcastJsonMessage(message);
        }
    }

    private User loginUser() {
        ActionMessage loginJson = readJsonFromStream(ActionMessage.class);
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

    private <T> T readJsonFromStream(Class<T> tClass) {
        return gson.fromJson(in.nextLine(), tClass);
    }

    public <T> void sendJson(T data) {
        String message = gson.toJson(data);
        out.println(message);
    }
}
