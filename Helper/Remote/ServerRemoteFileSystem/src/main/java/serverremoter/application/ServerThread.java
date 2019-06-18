package serverremoter.application;

import java.io.IOException;
import java.io.ObjectOutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;

import javax.swing.JTextArea;

import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;
import serverremoter.models.Message;
import serverremoter.service.Config;
import serverremoter.service.LoggerOnTextArea;

/*
 * Поток сервера
 */
public class ServerThread extends Thread {

	/*
	 * Сокет
	 */
	ServerSocket socket;
	/*
	 * Максимум
	 */
	final int MAXIMUM;
	/*
	 * Клиенты
	 */
	ArrayList<ClientThread> clientThreads = new ArrayList<ClientThread>();

	private LoggerOnTextArea loggerOnTextArea;

	/*
	 * Удалить клиента
	 */
	public void removeClient(ClientThread clientThread) {
		clientThreads.remove(clientThread);
	}

	private Config config;

	/*
	 * Конструктор
	 */
	public ServerThread(ServerSocket serverSocket, Config config) {
		MAXIMUM = config.getMaximum();
		this.socket = serverSocket;
		this.config = config;
	}

	/*
	 * Старт потока
	 */
	@Override
	public void run() {

		Socket input;
		try {
			loggerOnTextArea = LoggerOnTextArea.instance();
		} catch (Exception e) {
			e.printStackTrace();
		}
		while (true) {
			try {
				input = socket.accept();
				loggerOnTextArea.info("New user\n");
				if (clientThreads.size() == MAXIMUM) {
					loggerOnTextArea.info("Server is full\n");
					new ObjectOutputStream(input.getOutputStream())
							.writeObject(new Message(MessageInputType.Default, MessageOutputType.IsFull));
					input.close();
					continue;
				}
				loggerOnTextArea.info("Create new user's thread\n");
				clientThreads.add(new ClientThread(input, this, config));
			} catch (Exception ex) {
				loggerOnTextArea.error(ex);
				break;
			}

		}

	}

}
