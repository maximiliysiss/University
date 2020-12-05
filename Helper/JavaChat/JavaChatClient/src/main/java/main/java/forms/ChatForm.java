package main.java.forms;

import main.java.logic.ClientSocketLogic;

import javax.swing.*;
import java.awt.*;

public class ChatForm extends JFrame {

    private final int WIDTH = 600, HEIGHT = 480;

    private TextArea chatArea;
    private TextField chatInput;
    private TextArea onlineArea;

    private final ClientSocketLogic clientSocketLogic;

    public ChatForm(ClientSocketLogic clientSocketLogic) throws HeadlessException {
        this.clientSocketLogic = clientSocketLogic;

        initGUI();
    }

    private void initGUI() {
        JPanel rootPanel = new JPanel(new FlowLayout());
        rootPanel.setSize(WIDTH, HEIGHT);

        JPanel highLine = new JPanel(new FlowLayout());
        highLine.setSize(WIDTH, (int) (HEIGHT * 0.8));
        chatArea = new TextArea();
        chatArea.setSize((int) (WIDTH * 0.7), (int) (HEIGHT * 0.8));
        highLine.add(chatArea);
        onlineArea = new TextArea();
        onlineArea.setSize((int) (WIDTH * 0.3), (int) (HEIGHT * 0.8));
        highLine.add(onlineArea);

        JPanel lowLine = new JPanel(new FlowLayout());
        lowLine.setSize(WIDTH, (int) (HEIGHT * 0.2));
        chatInput = new TextField();
        chatInput.setSize((int) (WIDTH * 0.8), (int) (HEIGHT * 0.2));
        lowLine.add(chatInput);

        Button sendMessage = new Button("Send");
        sendMessage.setSize((int) (WIDTH * 0.2), (int) (HEIGHT * 0.2));
        lowLine.add(sendMessage);

        rootPanel.add(highLine);
        rootPanel.add(lowLine);

        setContentPane(rootPanel);

        setSize(WIDTH, HEIGHT);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    private void sendMessage() {

    }

    public void showDialog() {
        setVisible(true);
    }
}
