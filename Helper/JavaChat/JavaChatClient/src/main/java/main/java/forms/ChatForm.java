package main.java.forms;

import main.java.common.UserContext;
import main.java.logic.ClientSocketLogic;
import main.java.models.messages.ActionMessage;
import main.java.models.messages.Message;

import javax.swing.*;
import java.awt.*;

/**
 * Форма чата
 */
public class ChatForm extends JFrame {

    /**
     * Размеры
     */
    private final int WIDTH = 600, HEIGHT = 480;

    /**
     * Компоненты формы
     */
    private TextArea chatArea;
    private TextField chatInput;
    private TextArea onlineArea;
    private Button sendMessage;

    /**
     * Класс логики взаимодействия с сервером
     */
    private final ClientSocketLogic clientSocketLogic;

    public ChatForm(ClientSocketLogic clientSocketLogic) throws HeadlessException {
        this.clientSocketLogic = clientSocketLogic;

        initGUI();

        /**
         * Прикрепление обработчиков
         */
        clientSocketLogic.registerMessageHandler(this::onNewMessages);
        clientSocketLogic.registerLoadHandler(this::onLoadHistory);
        clientSocketLogic.registerFailLoginHandler(this::onFailLogin);
        clientSocketLogic.registerOnlineHandler(this::onOnlineChange);
    }

    /**
     * Обновления списка онлайн пользователей
     *
     * @param online
     */
    private void onOnlineChange(String online) {
        onlineArea.setText(online);
    }

    /**
     * Обработка ошибок авторизации
     *
     * @param message
     */
    private void onFailLogin(String message) {
        for (int i = 0; i < getMenuBar().getMenuCount(); i++)
            getMenuBar().getMenu(i).setEnabled(false);
        chatInput.setEditable(false);
        sendMessage.setEnabled(false);
        onNewMessages(message);
    }

    /**
     * Обработка нового сообщения
     *
     * @param s
     */
    private void onNewMessages(String s) {
        if (s == null || s.length() == 0)
            return;
        chatArea.append(s + "\n");
    }

    /**
     * Обработка загрузки истории
     *
     * @param s
     */
    private void onLoadHistory(String s) {
        s = s.trim();
        if (s.length() > 0)
            s += "\n";

        chatArea.setText(s);
    }

    /**
     * Создание UI
     */
    private void initGUI() {
        JPanel rootPanel = new JPanel(new FlowLayout());
        rootPanel.setPreferredSize(new Dimension(WIDTH, HEIGHT));

        JMenuBar menuBar = new JMenuBar();
        JMenu chatMenuItem = new JMenu("Chat");
        JMenuItem clearDataItem = new JMenuItem("Clear data");
        clearDataItem.addActionListener(x -> onClearData());
        chatMenuItem.add(clearDataItem);
        menuBar.add(chatMenuItem);

        JPanel highLine = new JPanel(new FlowLayout());
        highLine.setPreferredSize(new Dimension(WIDTH, (int) (HEIGHT * 0.76)));
        chatArea = new TextArea();
        chatArea.setPreferredSize(new Dimension((int) (WIDTH * 0.6), (int) (HEIGHT * 0.76)));
        chatArea.setEditable(false);
        highLine.add(chatArea);
        onlineArea = new TextArea();
        onlineArea.setPreferredSize(new Dimension((int) (WIDTH * 0.3), (int) (HEIGHT * 0.76)));
        onlineArea.setEditable(false);
        highLine.add(onlineArea);

        JPanel lowLine = new JPanel(new FlowLayout());
        lowLine.setPreferredSize(new Dimension(WIDTH, (int) (HEIGHT * 0.06)));
        chatInput = new TextField();
        chatInput.setPreferredSize(new Dimension((int) (WIDTH * 0.85), (int) (HEIGHT * 0.06)));
        lowLine.add(chatInput);

        sendMessage = new Button("Send");
        sendMessage.addActionListener(x -> sendMessage());
        lowLine.add(sendMessage);

        rootPanel.add(highLine);
        rootPanel.add(lowLine);

        setJMenuBar(menuBar);
        setContentPane(rootPanel);

        setSize(WIDTH, HEIGHT);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    /**
     * Кнопка очистка данных
     */
    private void onClearData() {
        clientSocketLogic.sendJsonMessage(new ActionMessage("clear"));
    }

    /**
     * Отправка сообщения
     */
    private void sendMessage() {
        String content = chatInput.getText();
        chatInput.setText(null);
        clientSocketLogic.sendJsonMessage(new Message(content, UserContext.getUserId(), UserContext.getLogin()));
    }

    /**
     * Показать форму
     */
    public void showDialog() {
        setVisible(true);
    }
}
