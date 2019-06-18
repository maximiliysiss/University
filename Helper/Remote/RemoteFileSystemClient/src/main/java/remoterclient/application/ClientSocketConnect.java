package remoterclient.application;

import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.nio.file.Path;

import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;
import serverremoter.models.FileSystemNodeRemote;
import serverremoter.models.Message;
import serverremoter.models.MessageChageName;
import serverremoter.models.MessageSize;
import serverremoter.service.Config;

/**
 * Подключение по сокетам к серверу
 * 
 * @author
 *
 */
public class ClientSocketConnect {

	/**
	 * Сокет
	 */
	Socket socket;

	/**
	 * Он один. Тут используется шаблон Singleton
	 */
	private static ClientSocketConnect clientSocketConnect;

	/**
	 * Получение единсвенного экземпляра
	 * 
	 * @param config
	 * @return
	 */
	public static ClientSocketConnect instance(Config config) {
		if (clientSocketConnect == null || clientSocketConnect.isDestroyed)
			clientSocketConnect = new ClientSocketConnect(config);
		return clientSocketConnect;
	}

	/**
	 * Получение единсвенного экземпляра
	 * 
	 * @return
	 */
	public static ClientSocketConnect instance() {
		return clientSocketConnect;
	}

	/**
	 * Потоки ввода и вывода
	 */
	ObjectInputStream inputStream;
	ObjectOutputStream outputStream;
	boolean isDestroyed = false;

	/**
	 * Конструктор
	 * 
	 * @param config
	 */
	private ClientSocketConnect(Config config) {
		try {
			socket = new Socket(config.getIp(), config.getPort());
			if (socket.isConnected()) {
				outputStream = new ObjectOutputStream(new DataOutputStream(this.socket.getOutputStream()));
				inputStream = new ObjectInputStream(new DataInputStream(this.socket.getInputStream()));

				sendMessageToServer(new Message());
			}
		} catch (IOException e) {
			System.out.println(e.getMessage());
		}
	}

	/**
	 * Отправить данные на сервер
	 * 
	 * @param <T>
	 * @param elem
	 */
	public <T> void sendMessageToServer(T elem) {
		try {
			outputStream.writeObject(elem);
		} catch (Exception ex) {
			System.out.println(ex.getMessage());
		}
	}

	/**
	 * Отправить файл
	 * 
	 * @param path
	 */
	public void sendFile(Path path) {
		File myFile = path.toFile();
		byte[] fileBytes = new byte[(int) myFile.length()];
		sendMessageToServer(new MessageChageName(MessageInputType.Upload, MessageOutputType.Success, myFile.getName()));
		sendMessageToServer(new MessageSize(fileBytes.length));
		try (FileInputStream fileInputStream = new FileInputStream(myFile)) {
			try (BufferedInputStream bufferedInputStream = new BufferedInputStream(fileInputStream)) {
				bufferedInputStream.read(fileBytes, 0, fileBytes.length);
				outputStream.write(fileBytes, 0, fileBytes.length);
				outputStream.flush();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * Загрузить файл
	 * 
	 * @param nodeRemote
	 * @return
	 */
	public byte[] loadFile(FileSystemNodeRemote nodeRemote) {
		sendMessageToServer(new Message(MessageInputType.Load, MessageOutputType.Success, nodeRemote));
		MessageSize messageSize = this.<MessageSize>getObject();
		if (messageSize.getValue() == 0)
			return null;
		byte[] bs = new byte[messageSize.getValue()];

		try {
			int bytesRead = inputStream.read(bs, 0, bs.length);
			int current = bytesRead;
			do {
				bytesRead = inputStream.read(bs, current, bs.length - current);
				if (bytesRead >= 0)
					current += bytesRead;
			} while (bytesRead > 0);

		} catch (IOException e) {
			e.printStackTrace();
		}
		return bs;
	}

	public void destroy() {
		sendMessageToServer(new Message(MessageInputType.End, MessageOutputType.Success));
		try {
			outputStream.close();
			inputStream.close();
			isDestroyed = true;
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Получить объект
	 * 
	 * @param <T>
	 * @return
	 */
	public <T> T getObject() {
		try {
			return (T) inputStream.readObject();
		} catch (ClassNotFoundException | IOException e) {
			e.printStackTrace();
		}
		return null;
	}

}
