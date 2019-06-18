package serverremoter.service;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import serverremoter.models.FileSystemFolderRemote;
import serverremoter.models.FileSystemNodeRemote;

/**
 * Сервис для работы с файлами
 * @author
 *
 */
public class FileSystemService {

	/**
	 * Логгер
	 * @return
	 */
	public static LoggerOnTextArea getLogger() {
		try {
			return LoggerOnTextArea.instance();
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	/**
	 * Узнать размер файла
	 * @param path
	 * @return
	 */
	public static long fileSize(String path) {
		File file = new File(path);
		if (!file.exists())
			return -1;
		return file.length();
	}

	/**
	 * Удалить
	 * @param path
	 */
	public static void delete(String path) {
		try {
			for (String in : new File(path).list()) {
				if (Paths.get(in).toFile().isDirectory())
					delete(path + "//" + in);
				else
					Files.delete(Paths.get(path, in));
			}
			Files.delete(Paths.get(path));
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Получить инфомрациб о структуре
	 * @param path
	 * @return
	 */
	public static java.util.List<FileSystemNodeRemote> getInnerElements(String path) {
		ArrayList<FileSystemNodeRemote> list = new ArrayList<FileSystemNodeRemote>();
		for (String name : Paths.get(path).toFile().list()) {
			File pathIn = Paths.get(path, name).toFile();
			if (pathIn.isDirectory())
				list.add(new FileSystemFolderRemote(pathIn.toPath()));
			else
				list.add(new FileSystemNodeRemote(pathIn.toPath()));
		}
		return list;
	}
}
