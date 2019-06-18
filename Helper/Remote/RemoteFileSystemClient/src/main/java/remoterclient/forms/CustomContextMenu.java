package remoterclient.forms;

import javax.swing.JMenuItem;
import javax.swing.JPopupMenu;

import remoterclient.application.ClientSocketConnect;
import serverremoter.models.FileSystemNodeRemote;

/**
 * Класс для контекстного меню на ПКМ
 * @author
 *
 */
public abstract class CustomContextMenu extends JPopupMenu {

	/**
	 * Для какого элемента
	 */
	protected FileSystemNodeRemote fileSystemNodeRemote;
	/**
	 * Подключение к сокетам
	 */
	protected ClientSocketConnect clientSocketConnect;
	/**
	 * Форма клиента
	 */
	protected ClientForm clientForm;

	/**
	 * Установка клиентской формы
	 * @param clientForm
	 * @return
	 */
	public CustomContextMenu setClientForm(ClientForm clientForm) {
		this.clientForm = clientForm;
		return this;
	}
	
	/**
	 * Перерисовать форму
	 */
	public void generate() {
		clientForm.generateLayout();
	}

	/**
	 * Установка и получение полей
	 * @return
	 */
	public FileSystemNodeRemote getFileSystemNodeRemote() {
		return fileSystemNodeRemote;
	}

	public void setFileSystemNodeRemote(FileSystemNodeRemote fileSystemNodeRemote) {
		this.fileSystemNodeRemote = fileSystemNodeRemote;
	}

	/**
	 * Конструктор
	 * @param fileSystemNodeRemote
	 */
	public CustomContextMenu(FileSystemNodeRemote fileSystemNodeRemote) {
		this.fileSystemNodeRemote = fileSystemNodeRemote;
		this.clientSocketConnect = ClientSocketConnect.instance();
		createMenu();
	}

	/**
	 * Создать меню
	 */
	abstract void createMenu();
}
