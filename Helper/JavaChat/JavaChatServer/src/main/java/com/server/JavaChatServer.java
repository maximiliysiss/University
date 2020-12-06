package com.server;

import com.server.common.Config;
import com.server.common.Cryptographic;
import com.server.data.SqlInteractor;
import com.server.data.SqlInteractorFactory;
import com.server.logic.ChatServer;

import java.util.Scanner;

public class JavaChatServer {

    public static void main(String[] args) {

        System.out.println("Start server");

        Config config = Config.readConfig("config.json");
        Cryptographic.init(config.getKey());

        SqlInteractor sqlInteractor = SqlInteractorFactory.create(config.getConnectionString());
        ChatServer chatServer = new ChatServer(config.getIp(), config.getPort(), sqlInteractor, config.getMaxPool());
        chatServer.start();

        System.out.println("Press Enter to exit");
        Scanner scanner = new Scanner(System.in);
        scanner.nextLine();
        chatServer.shutdown();
    }

}
