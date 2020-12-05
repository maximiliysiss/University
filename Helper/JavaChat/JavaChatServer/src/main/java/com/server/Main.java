package com.server;

import com.server.common.Cryptographic;
import com.server.data.SqlInteractor;
import com.server.data.SqlInteractorFactory;
import com.server.logic.ChatServer;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        System.out.println("Enter ip/port:");

        Scanner sc = new Scanner(System.in);
        String ip = sc.next();
        int port = sc.nextInt();
        sc.nextLine();

        Cryptographic.init("Some key");

        SqlInteractor sqlInteractor = SqlInteractorFactory.create("jdbc:sqlserver://localhost,54813;integratedSecurity=true;");
        ChatServer chatServer = new ChatServer(ip, port, sqlInteractor, 50);
        chatServer.start();

        System.out.println("Press Enter to exit");
        sc.nextLine();
    }
}
