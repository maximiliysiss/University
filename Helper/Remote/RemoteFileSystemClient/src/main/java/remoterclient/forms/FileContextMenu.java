package remoterclient.forms;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.BufferedOutputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

import javax.swing.JFileChooser;
import javax.swing.JMenuItem;

import remoterclient.listener.TextInputHandler;
import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;
import serverremoter.models.FileSystemNodeRemote;
import serverremoter.models.Message;
import serverremoter.models.MessageChageName;

/**
 * КОнтекстное меню для файла
 * @author
 *
 */
public class FileContextMenu extends CustomContextMenu {

	/**
	 * Конструктор
	 * @param fileSystemNodeRemote
	 */
	public FileContextMenu(FileSystemNodeRemote fileSystemNodeRemote) {
		super(fileSystemNodeRemote);
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
		JMenuItem loadItem = new JMenuItem("Загрузить");
		loadItem.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent e) {
				JFileChooser fc = new JFileChooser();
				fc.setFileSelectionMode(JFileChooser.FILES_ONLY);
				if (fc.showSaveDialog(clientForm.frame) == JFileChooser.APPROVE_OPTION) {
					String path = fc.getSelectedFile().getAbsolutePath();
					byte[] bytes = clientSocketConnect.loadFile(fileSystemNodeRemote);
					if (bytes == null) {
						try {
							Files.createFile(Paths.get(path));
						} catch (IOException e1) {
							e1.printStackTrace();
						}
					} else
						try (FileOutputStream fileOutputStream = new FileOutputStream(path)) {
							try (BufferedOutputStream bufferedOutputStream = new BufferedOutputStream(
									fileOutputStream)) {
								bufferedOutputStream.write(bytes, 0, bytes.length);
								bufferedOutputStream.flush();
							}
						} catch (Exception ex) {
							ex.printStackTrace();
						}
				}
			}
		});
		add(editItem);
		add(copyItem);
		add(moveItem);
		add(deleteItem);
		add(loadItem);
	}
}
