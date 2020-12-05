package main.java;

import main.java.forms.LoginForm;
import main.java.logic.ClientSocketLogic;

import java.util.Scanner;

public class ChatClient {

    public static void main(String[] args) {

        System.out.println("Enter ip/port:");
        Scanner scanner = new Scanner(System.in);
        String ip = scanner.next();
        int port = Integer.parseInt(scanner.next());

        LoginForm loginForm = new LoginForm(new ClientSocketLogic(ip, port));
        loginForm.showDialog();
    }

}
