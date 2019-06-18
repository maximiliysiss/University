package remoterclient.forms;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.nio.file.Path;
import java.nio.file.Paths;

import javax.swing.JFileChooser;
import javax.swing.JMenuItem;

import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;
import serverremoter.models.FileSystemNodeRemote;
import serverremoter.models.Message;

/**
 * КОнтекстное меню для всей области папки (окно)
 * @author
 *
 */
public class ContextMenu extends CustomContextMenu {

	/**
	 * Конструктор
	 * @param fileSystemNodeRemote
	 */
	public ContextMenu(FileSystemNodeRemote fileSystemNodeRemote) {
		super(fileSystemNodeRemote);
	}

	/**
	 * Создать меню
	 */
	@Override
	void createMenu() {
		JMenuItem reloadItem = new JMenuItem("Перезагрузить");
		reloadItem.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent e) {
				generate();
			}
		});
		JMenuItem pasteItem = new JMenuItem("Вставить");
		pasteItem.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent e) {
				clientSocketConnect.sendMessageToServer(
						new Message(MessageInputType.Paste, MessageOutputType.Success, fileSystemNodeRemote));
				generate();
			}
		});
		JMenuItem uploadItem = new JMenuItem("Выгрузить");
		uploadItem.addActionListener(new ActionListener() {

			@Override
			public void actionPerformed(ActionEvent e) {
				JFileChooser fc = new JFileChooser();
				fc.setFileSelectionMode(JFileChooser.FILES_ONLY);
				if (fc.showOpenDialog(clientForm.frame) == JFileChooser.APPROVE_OPTION) {
					String path = fc.getSelectedFile().getAbsolutePath();
					clientSocketConnect.sendFile(Paths.get(path));
				}
				generate();
			}
		});
		add(reloadItem);
		add(pasteItem);
		add(uploadItem);
	}

}
