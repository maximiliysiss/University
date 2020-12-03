package main.java.forms;

import javax.swing.*;
import java.awt.*;

public class LoginForm extends JFrame {

    private JPanel rootPanel;
    private TextField loginField;
    private JPasswordField passwordField;

    private final int WIDTH = 300;
    private final int HEIGHT = 200;

    public LoginForm() {
        initGUI();
    }

    private void initGUI() {

        GridLayout gridLayout = new GridLayout(6, 1);
        rootPanel = new JPanel(gridLayout);

        Button loginButton = new Button("Login");
        Button registerButton = new Button("Register");

        loginButton.addActionListener(x -> onLogin());
        registerButton.addActionListener(x -> onRegister());

        rootPanel.add(new Label("Login"));
        rootPanel.add(loginField = new TextField());
        rootPanel.add(new Label("Password"));
        rootPanel.add(passwordField = new JPasswordField());
        rootPanel.add(loginButton);
        rootPanel.add(registerButton);

        setContentPane(rootPanel);
        setSize(WIDTH, HEIGHT);
        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
    }

    private void onLogin() {

    }

    private void onRegister() {

    }

    public void showDialog() {
        setVisible(true);
    }
}
