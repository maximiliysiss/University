package serverremoter.application;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.nio.file.CopyOption;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;

import org.apache.commons.io.FileUtils;

import serverremoter.enums.BufferType;
import serverremoter.enums.FileSystemNodeType;
import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;
import serverremoter.models.BufferFileSystem;
import serverremoter.models.FileSystemFolderRemote;
import serverremoter.models.FileSystemNodeRemote;
import serverremoter.models.Message;
import serverremoter.models.MessageChageName;
import serverremoter.models.MessageSize;
import serverremoter.service.Config;
import serverremoter.service.CustomFilter;
import serverremoter.service.FileSystemService;
import serverremoter.service.IpFilterComparer;
import serverremoter.service.LoggerOnTextArea;

/*
 * Поток клиента
 */
public class ClientThread extends Thread {

	/*
	 * Сокет
	 */
	Socket socket;
	String topPath;
	/*
	 * Поток сервера
	 */
	ServerThread serverThread;
	Config config;
	/*
	 * Логгер
	 */
	LoggerOnTextArea loggerOnTextArea;
	/**
	 * Текущая папка
	 */
	FileSystemFolderRemote currentFolder;
	/**
	 * Буффер для хранения Copy-Paste
	 */
	BufferFileSystem bufferFileSystem;

	/*
	 * Конструктор
	 */
	public ClientThread(Socket socket, ServerThread serverThread, Config config) {
		this.socket = socket;
		this.serverThread = serverThread;
		this.config = config;
		for (CustomFilter filter : config.getFilters()) {
			if (IpFilterComparer.Compare(filter.getIp(), socket.getInetAddress().toString().substring(1))) {
				this.currentFolder = new FileSystemFolderRemote(Paths.get(filter.getPath()));
				topPath = filter.getPath();
			}
		}
		if (currentFolder == null) {
			this.currentFolder = new FileSystemFolderRemote(Paths.get(config.getPath()));
			topPath = config.getPath();
		}
		this.currentFolder.setTop(isTop(Paths.get(currentFolder.getFullName())));
		this.start();
	}

	/*
	 * Потоки
	 */
	ObjectInputStream inputStream;
	ObjectOutputStream outputStream;

	/*
	 * Отправить сообщение клиенту
	 */
	public <T> void sendMessageToClient(T message) {
		try {
			outputStream.writeObject(message);
		} catch (Exception ex) {
			loggerOnTextArea.error(ex);
		}
	}

	/*
	 * Отключить сокет
	 */
	public void destroy() {
		try {
			socket.close();
			serverThread.removeClient(this);
		} catch (Exception ex) {
			loggerOnTextArea.error(ex);
		}

	}

	/**
	 * Текщая папка в топе
	 * 
	 * @param path
	 * @return
	 */
	private boolean isTop(Path path) {
		return Paths.get(topPath).toAbsolutePath().toString().equals(path.toAbsolutePath().toString());
	}

	/*
	 * Запуск потока
	 */
	@Override
	public void run() {

		try {

			loggerOnTextArea = LoggerOnTextArea.instance();

			loggerOnTextArea.info("Start thread\n");
			inputStream = new ObjectInputStream(this.socket.getInputStream());
			outputStream = new ObjectOutputStream(this.socket.getOutputStream());
			Message messageInput = (Message) inputStream.readObject();
			loggerOnTextArea.info("Success new user\n");

			while (true) {

				messageInput = (Message) inputStream.readObject();

				switch (messageInput.getInputType()) {
				case Delete:
					if (messageInput.getFileSystemNodeRemote().getNodeType() == FileSystemNodeType.File)
						Files.delete(Paths.get(messageInput.getFileSystemNodeRemote().getFullName()));
					else
						FileSystemService.delete(messageInput.getFileSystemNodeRemote().getFullName());
					break;
				case Down:
					currentFolder = new FileSystemFolderRemote(
							Paths.get(messageInput.getFileSystemNodeRemote().getFullName()));
					currentFolder.setTop(isTop(Paths.get(currentFolder.getFullName())));
					break;
				case Current:
					currentFolder = new FileSystemFolderRemote(Paths.get(currentFolder.getFullName()));
					currentFolder.setTop(isTop(Paths.get(currentFolder.getFullName())));
					sendMessageToClient(currentFolder);
					break;
				case Load: {
					File file = new File(messageInput.getFileSystemNodeRemote().getFullName());
					sendMessageToClient(new MessageSize((int) file.length()));
					if (file.length() != 0) {
						byte[] fileBytes = new byte[(int) file.length()];
						try (FileInputStream fileInputStream = new FileInputStream(file)) {
							try (BufferedInputStream bufferedInputStream = new BufferedInputStream(fileInputStream)) {
								bufferedInputStream.read(fileBytes, 0, fileBytes.length);
								outputStream.write(fileBytes, 0, fileBytes.length);
								outputStream.flush();
							}
						} catch (Exception e) {
							e.printStackTrace();
						}
					}
					break;
				}
				case Move:
					bufferFileSystem = new BufferFileSystem(messageInput.getFileSystemNodeRemote(), BufferType.Move);
					break;
				case Rename:
					Paths.get(messageInput.getFileSystemNodeRemote().getFullName()).toFile().renameTo(Paths
							.get(Paths.get(messageInput.getFileSystemNodeRemote().getFullName()).getParent().toString(),
									((MessageChageName) messageInput).getNewName())
							.toFile());
					break;
				case Up:
					currentFolder = new FileSystemFolderRemote(
							Paths.get(messageInput.getFileSystemNodeRemote().getFullName()).getParent());
					currentFolder.setTop(isTop(Paths.get(currentFolder.getFullName())));
					break;
				case Upload: {
					String fileName = ((MessageChageName) messageInput).getNewName();
					MessageSize messageSize = (MessageSize) inputStream.readObject();
					byte[] bytes = new byte[messageSize.getValue()];
					try (FileOutputStream fileOutputStream = new FileOutputStream(
							Paths.get(currentFolder.getFullName(), fileName).toString())) {
						try (BufferedOutputStream bufferedOutputStream = new BufferedOutputStream(fileOutputStream)) {
							int bytesRead = inputStream.read(bytes, 0, bytes.length);
							int current = bytesRead;
							do {
								bytesRead = inputStream.read(bytes, current, bytes.length - current);
								if (bytesRead >= 0)
									current += bytesRead;
							} while (bytesRead > 0);

							bufferedOutputStream.write(bytes, 0, current);
							bufferedOutputStream.flush();
						}
					}

					break;
				}
				case Copy:
					bufferFileSystem = new BufferFileSystem(messageInput.getFileSystemNodeRemote(), BufferType.Copy);
					break;
				case Paste:
					if (bufferFileSystem != null) {
						switch (bufferFileSystem.getBufferType()) {
						case Copy:
							Files.copy(Paths.get(bufferFileSystem.getFileSystemNodeRemote().getFullName()),
									Paths.get(currentFolder.getFullName() + "/"
											+ Paths.get(bufferFileSystem.getFileSystemNodeRemote().getFullName())
													.getFileName().toString()));
							break;
						case Move:
							Files.move(Paths.get(bufferFileSystem.getFileSystemNodeRemote().getFullName()),
									Paths.get(currentFolder.getFullName() + "/"
											+ Paths.get(bufferFileSystem.getFileSystemNodeRemote().getFullName())
													.getFileName().toString()));
							break;
						}
					}
					break;
				case End:
					destroy();
					return;
				}
			}

		} catch (Exception ex) {
			loggerOnTextArea.error(ex);
		}

	}

}
