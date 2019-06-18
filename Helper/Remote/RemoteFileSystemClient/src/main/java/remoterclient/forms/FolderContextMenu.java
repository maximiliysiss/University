package remoterclient.forms;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JMenuItem;

import remoterclient.listener.TextInputHandler;
import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;
import serverremoter.models.FileSystemNodeRemote;
import serverremoter.models.Message;
import serverremoter.models.MessageChageName;

/**
 * Контекстное меню для папки
 * @author
 *
 */
public class FolderContextMenu extends CustomContextMenu {

	/**
	 * Конструктор
	 * @param fileSystemNodeRemote
	 */
	public FolderContextMenu(FileSystemNodeRemote fileSystemNodeRemote) {
		super(fileSystemNodeRemote);
		// TODO Auto-generated constructor stub
	}

	/**
	 * Создать меню
	 */
	@Override
	void createMenu() {
		JMenuItem editItem = new JMenuItem("Изменить");
		editItem.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				new EnterDataForm(new TextInputHandler() {
					@Override
					public void onInputText(String text) {
						clientSocketConnect.sendMessageToServer(new MessageChageName(MessageInputType.Rename,
								MessageOutputType.Success, text, fileSystemNodeRemote));
						generate();
					}
				});
			}
		});
		JMenuItem copyItem = new JMenuItem("Копировать");
		copyItem.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent e) {
				clientSocketConnect.sendMessageToServer(
						new Message(MessageInputType.Copy, MessageOutputType.Success, fileSystemNodeRemote));
			}
		});
		JMenuItem moveItem = new JMenuItem("Переместить");
		moveItem.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent e) {
				clientSocketConnect.sendMessageToServer(
						new Message(MessageInputType.Move, MessageOutputType.Success, fileSystemNodeRemote));
			}
		});
		JMenuItem deleteItem = new JMenuItem("Удалить");
		deleteItem.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent e) {
				clientSocketConnect.sendMessageToServer(
						new Message(MessageInputType.Delete, MessageOutputType.Success, fileSystemNodeRemote));
				generate();
			}
		});
		add(editItem);
		add(copyItem);
		add(moveItem);
		add(deleteItem);
	}

}
