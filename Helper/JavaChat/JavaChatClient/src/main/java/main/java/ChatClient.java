package main.java;

import main.java.common.Config;
import main.java.common.Cryptographic;
import main.java.forms.LoginForm;
import main.java.logic.ClientSocketLogic;

public class ChatClient {

    public static void main(String[] args) {

        System.out.println("Start client");

        Config config = Config.readConfig("config.json");
        Cryptographic.init(config.getKey());

        LoginForm loginForm = new LoginForm(new ClientSocketLogic(config.getIp(), config.getPort()));
        loginForm.showDialog();
    }

}
