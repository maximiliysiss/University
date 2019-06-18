package serverremoter.forms;

import javax.swing.JFrame;
import javax.swing.JButton;
import serverremoter.application.ServerThread;
import serverremoter.service.Config;
import serverremoter.service.LoggerOnTextArea;

import javax.swing.JLabel;
import javax.swing.JScrollPane;

import java.awt.event.ActionListener;
import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.awt.event.ActionEvent;
import javax.swing.JTextArea;
import javax.swing.JTextField;

/*
 * Форма сервера
 */
public class ServerForm {
	/*
	 * Форма
	 */
	JFrame frame = new JFrame();
	/*
	 * Конфигурация
	 */
	Config config;

	/*
	 * Сокет
	 */
	ServerSocket socketListener;
	/*
	 * Поток сервера
	 */
	ServerThread serverThread;
	/*
	 * Вывод
	 */
	JTextArea textArea;

	/*
	 * Конструктор
	 */
	public ServerForm(Config config) {

		this.config = config;
		InitForm();

	}

	/*
	 * Старт сервера
	 */
	private void onStart() {
		if (socketListener == null) {
			try {
				textArea.append("Create socket listener on " + config.getIp() + ":" + config.getPort() + "\n");
				socketListener = new ServerSocket();
				socketListener.setReuseAddress(true);
				socketListener.bind(new InetSocketAddress(config.getIp(), config.getPort()));

			} catch (Exception e) {
				loggerOnTextArea.error(e);
			}
		}

		serverThread = new ServerThread(socketListener, config);
		serverThread.start();
	}

	/*
	 * Остановка потока
	 */
	private void onStop() {
		textArea.append("Stop server");
		if (serverThread != null) {
			try {
				serverThread.interrupt();
				serverThread = null;
				socketListener.close();
			} catch (IOException e) {
				loggerOnTextArea.error(e);
			}

		}
	}

	/*
	 * Создание формы
	 */
	private void InitForm() {

		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.getContentPane().setLayout(null);
		frame.setSize(600, 400);

		JButton btnStart = new JButton("Start");
		JButton btnStop = new JButton("Stop");
		btnStart.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				config.setIp(ipTextField.getText());
				config.setPort(Integer.parseInt(portTextField.getText()));
				onStart();
				btnStart.setEnabled(false);
				btnStop.setEnabled(true);
			}
		});
		btnStart.setBounds(10, 11, 89, 23);
		frame.getContentPane().add(btnStart);

		btnStop.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				onStop();
				btnStart.setEnabled(true);
				btnStop.setEnabled(false);
			}
		});
		btnStop.setEnabled(false);
		btnStop.setBounds(109, 11, 89, 23);
		frame.getContentPane().add(btnStop);

		JLabel lblLogs = new JLabel("Logs");
		lblLogs.setBounds(10, 40, 46, 14);
		frame.getContentPane().add(lblLogs);

		JScrollPane scrollPane = new JScrollPane();
		scrollPane.setBounds(71, 65, 503, 285);
		frame.getContentPane().add(scrollPane);
		textArea = new JTextArea(10, 20);
		scrollPane.setViewportView(textArea);
		textArea.setEditable(false);

		loggerOnTextArea = LoggerOnTextArea.instance(textArea);

		ipTextField = new JTextField(config.getIp());
		ipTextField.setBounds(225, 12, 86, 20);
		frame.getContentPane().add(ipTextField);
		ipTextField.setColumns(10);

		portTextField = new JTextField(String.valueOf(config.getPort()));
		portTextField.setBounds(346, 12, 86, 20);
		frame.getContentPane().add(portTextField);
		portTextField.setColumns(10);

		frame.setVisible(true);

	}

	/*
	 * Логгер
	 */
	private LoggerOnTextArea loggerOnTextArea;
	private JTextField ipTextField;
	private JTextField portTextField;
}
