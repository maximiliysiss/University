package serverremoter.models;

import java.nio.file.Path;
import java.util.List;

import serverremoter.enums.FileSystemNodeType;
import serverremoter.service.FileSystemService;

/**
 * Узел в виде папки
 * @author
 *
 */
public class FileSystemFolderRemote extends FileSystemNodeRemote {

	/**
	 * То, что лежит внутри папки
	 */
	List<FileSystemNodeRemote> nodeRemotes;
	/**
	 * Самый root дерева?
	 */
	boolean isTop = false;

	/**
	 * Устнаовка и получение полей
	 * @return
	 */
	public boolean isTop() {
		return isTop;
	}

	public void setTop(boolean isTop) {
		this.isTop = isTop;
	}

	public FileSystemFolderRemote() {
		super();
		nodeType = FileSystemNodeType.Folder;
	}

	/**
	 * Конструктор
	 * @param id
	 * @param name
	 * @param size
	 * @param fullName
	 */
	public FileSystemFolderRemote(int id, String name, float size, String fullName) {
		super(id, name, size, fullName);
		nodeType = FileSystemNodeType.Folder;
		nodeRemotes = FileSystemService.getInnerElements(this.getFullName());
	}
	
	/**
	 * олучение и установка полей
	 * @return
	 */
	public List<FileSystemNodeRemote> getNodeRemotes() {
		return nodeRemotes;
	}

	public void setNodeRemotes(List<FileSystemNodeRemote> nodeRemotes) {
		this.nodeRemotes = nodeRemotes;
	}

	/**
	 * Конструкторы
	 * @param path
	 */
	public FileSystemFolderRemote(Path path) {
		super(path);
		nodeType = FileSystemNodeType.Folder;
		nodeRemotes = FileSystemService.getInnerElements(this.getFullName());
	}

	public FileSystemFolderRemote(String name, float size, String fullName) {
		super(name, size, fullName);
		nodeType = FileSystemNodeType.Folder;
		nodeRemotes = FileSystemService.getInnerElements(this.getFullName());
	}
}
