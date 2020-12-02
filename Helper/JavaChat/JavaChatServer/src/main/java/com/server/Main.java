package com.server;

import com.server.data.MSSQLInteractor;
import com.server.data.SqlInteractor;
import com.server.logic.ChatServer;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        System.out.println("Enter ip/port:");

        Scanner sc = new Scanner(System.in);
        String ip = sc.next();
        int port = sc.nextInt();
        sc.nextLine();

        SqlInteractor sqlInteractor = new MSSQLInteractor("");
        ChatServer chatServer = new ChatServer(ip, port, sqlInteractor, 50);
        chatServer.start();

        System.out.println("Press Enter to exit");
        sc.nextLine();
    }
}
