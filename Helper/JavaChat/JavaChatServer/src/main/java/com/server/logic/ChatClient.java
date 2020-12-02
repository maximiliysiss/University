package com.server.logic;

import com.google.gson.Gson;
import com.server.data.SqlInteractor;
import com.server.models.*;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;

public class ChatClient extends Thread {
    private final Scanner in;
    private final PrintWriter out;

    private final Socket clientSocket;

    private final Serverable serverable;
    private final SqlInteractor sqlInteractor;

    private final Gson gson = new Gson();
    private User user;

    public ChatClient(Socket clientSocket, SqlInteractor sqlInteractor, Serverable serverable) throws IOException {
        this.clientSocket = clientSocket;
        this.serverable = serverable;
        this.sqlInteractor = sqlInteractor;
        this.in = new Scanner(clientSocket.getInputStream());
        this.out = new PrintWriter(clientSocket.getOutputStream());
    }

    @Override
    public void run() {
        super.run();

        LoginMessage loginJson = readJsonFromStream(LoginMessage.class);
        Integer userId = null;
        if (loginJson.getAction().equals("login"))
            userId = sqlInteractor.tryLogin(loginJson.getLogin(), loginJson.getPassword());
        else
            userId = sqlInteractor.tryRegister(loginJson.getLogin(), loginJson.getPassword());

        if (userId == null) {
            sendJson(new ActionMessage("loginfail", "User not found / This user is registered yet"));
        }

        sendJson(new LoginResult(userId, loginJson.getLogin()));

        while (true) {
            if (!in.hasNext())
                continue;

            
        }
    }

    private <T> T readJsonFromStream(Class<T> tClass) {
        return gson.fromJson(in.nextLine(), tClass);
    }

    public <T> void sendJson(T data) {
        String message = gson.toJson(data);
        out.print(message);
    }
}
