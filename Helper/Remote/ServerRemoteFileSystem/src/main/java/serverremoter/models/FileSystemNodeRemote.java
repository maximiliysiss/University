package serverremoter.models;

import java.io.Serializable;
import java.nio.file.Path;

import serverremoter.enums.FileSystemNodeType;
import serverremoter.service.FileSystemService;

/**
 * Низший узел (файл)
 * @author
 *
 */
public class FileSystemNodeRemote implements Serializable {
	/**
	 * Название
	 */
	protected String name;
	/**
	 * Размер
	 */
	protected float size;
	/**
	 * Полный путь
	 */
	protected String fullName;
	/**
	 * Тип
	 */
	protected FileSystemNodeType nodeType;

	/**
	 * Конструкторы и установка и полкчение полей
	 */
	public FileSystemNodeRemote() {
		nodeType = FileSystemNodeType.File;
	}

	public FileSystemNodeType getNodeType() {
		return nodeType;
	}

	public void setNodeType(FileSystemNodeType nodeType) {
		this.nodeType = nodeType;
	}

	public FileSystemNodeRemote(Path path) {
		nodeType = FileSystemNodeType.File;
		this.name = path.getFileName().toString();
		this.fullName = path.toAbsolutePath().toString();
		this.size = FileSystemService.fileSize(fullName);
	}

	public FileSystemNodeRemote(int id, String name, float size, String fullName) {
		super();
		nodeType = FileSystemNodeType.File;
		this.name = name;
		this.size = size;
		this.fullName = fullName;
	}

	public FileSystemNodeRemote(String name, float size, String fullName) {
		super();
		nodeType = FileSystemNodeType.File;
		this.name = name;
		this.size = size;
		this.fullName = fullName;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public float getSize() {
		return size;
	}

	public void setSize(float size) {
		this.size = size;
	}

	public String getFullName() {
		return fullName;
	}

	public void setFullName(String fullName) {
		this.fullName = fullName;
	}

}
