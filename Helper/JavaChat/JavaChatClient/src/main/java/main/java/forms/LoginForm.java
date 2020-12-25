package main.java.forms;

import main.java.logic.ActionHandler;
import main.java.logic.ClientSocketLogic;

import javax.swing.*;
import java.awt.*;

/**
 * Форма входа
 */
public class LoginForm extends JFrame {

    /**
     * Класс логики
     */
    private final ClientSocketLogic clientSocketLogic;

    private JPanel rootPanel;
    private TextField loginField;
    private JPasswordField passwordField;

    /**
     * Размеры
     */
    private final int WIDTH = 300;
    private final int HEIGHT = 200;

    public LoginForm(ClientSocketLogic clientSocketLogic) {
        this.clientSocketLogic = clientSocketLogic;

        initGUI();
    }

    /**
     * Создание UI
     */
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

    /**
     * Кнопка Login
     */
    private void onLogin() {
        showChatForm(x -> clientSocketLogic.login(loginField.getText(), passwordField.getText()));
    }

    /**
     * Кнопка Register
     */
    private void onRegister() {
        showChatForm(x -> clientSocketLogic.register(loginField.getText(), passwordField.getText()));
    }

    /**
     * Показать форму чата
     * @param action
     */
    private void showChatForm(ActionHandler<Void> action) {
        setVisible(false);
        dispose();

        ChatForm chatForm = new ChatForm(this.clientSocketLogic);
        action.handle(null);
        chatForm.showDialog();
    }

    public void showDialog() {
        setVisible(true);
    }
}
