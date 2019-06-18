package remoterclient.forms;

import java.awt.Color;
import java.awt.Component;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.io.File;

import javax.imageio.ImageIO;
import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

import remoterclient.application.ClientSocketConnect;
import remoterclient.listener.DoubleClickHandler;
import remoterclient.listener.DoubleClickListener;
import serverremoter.enums.FileSystemNodeType;
import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;
import serverremoter.models.FileSystemFolderRemote;
import serverremoter.models.FileSystemNodeRemote;
import serverremoter.models.Message;
import serverremoter.service.Config;

/**
 * Форма клиента
 * 
 * @author
 *
 */
public class ClientForm {

	/**
	 * Элементы формы
	 */
	JFrame frame = new JFrame();
	JPanel jPanelMain = new JPanel();
	JPanel contentArea = new JPanel();
	JButton backBtn;
	JLabel pathLabel = new JLabel();
	/**
	 * Подключение по сокету
	 */
	ClientSocketConnect clientSocketConnect;
	/**
	 * Текущая папка
	 */
	FileSystemFolderRemote currentFolder;

	/**
	 * Отрисовать папку в окне
	 */
	public void generateLayout() {
		loadCurrent();
		pathLabel.setText(currentFolder.getFullName());
		contentArea.removeAll();
		if (currentFolder.isTop())
			backBtn.setVisible(false);
		else
			backBtn.setVisible(true);
		for (FileSystemNodeRemote remote : currentFolder.getNodeRemotes()) {
			try {
				JPanel jPanel = new JPanel();
				jPanel.setBackground(Color.white);
				jPanel.setLayout(new BoxLayout(jPanel, BoxLayout.Y_AXIS));
				BufferedImage img = null;
				switch (remote.getNodeType()) {
				case File:
					img = ImageIO.read(new File("src/main/resources/file.png"));
					break;
				case Folder:
					img = ImageIO.read(new File("src/main/resources/folder.png"));
					break;
				}
				ImageIcon icon = new ImageIcon(img);
				JLabel image = new JLabel(new ImageIcon(icon.getImage().getScaledInstance(64, 64, Image.SCALE_SMOOTH)));
				image.setAlignmentX(Component.CENTER_ALIGNMENT);
				image.setAlignmentY(Component.CENTER_ALIGNMENT);
				jPanel.add(image);
				JLabel name = new JLabel(remote.getName());
				name.setAlignmentX(Component.CENTER_ALIGNMENT);
				name.setAlignmentY(Component.CENTER_ALIGNMENT);
				jPanel.add(name);
				if (remote.getNodeType() == FileSystemNodeType.Folder) {
					jPanel.addMouseListener(new DoubleClickListener(new DoubleClickHandler() {
						@Override
						public void click() {
							clientSocketConnect.sendMessageToServer(
									new Message(MessageInputType.Down, MessageOutputType.Success, remote));
							generateLayout();
						}
					}, new FolderContextMenu(remote).setClientForm(this)));
				} else
					jPanel.addMouseListener(
							new DoubleClickListener(null, new FileContextMenu(remote).setClientForm(this)));
				contentArea.add(jPanel);
			} catch (Exception ex) {
			}
		}
		contentArea.revalidate();
		contentArea.repaint();
	}

	/**
	 * Загрузить информацию о текущей папке
	 */
	public void loadCurrent() {
		currentFolder = null;
		clientSocketConnect.sendMessageToServer(new Message(MessageInputType.Current, MessageOutputType.Success));
		currentFolder = clientSocketConnect.<FileSystemFolderRemote>getObject();
		System.out.println(currentFolder.getNodeRemotes().size());
	}

	/*
	 * Конструктор
	 */
	public ClientForm(Config config) {
		clientSocketConnect = ClientSocketConnect.instance(config);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setSize(600, 400);
		contentArea.setLayout(new GridLayout(0, 5, 10, 10));
		contentArea.setBackground(Color.white);
		jPanelMain.setLayout(new GridBagLayout());
		jPanelMain.setBackground(Color.white);
		GridBagConstraints bagConstraints = new GridBagConstraints();
		bagConstraints.gridy = 0;
		bagConstraints.gridx = 0;
		bagConstraints.weighty = 0.01;
		bagConstraints.weightx = 2;
		bagConstraints.anchor = GridBagConstraints.WEST;
		JPanel top = new JPanel();
		top.setLayout(new GridLayout(1, 4));
		JTextField ipTextField = new JTextField(config.getIp());
		JTextField portTextField = new JTextField(String.valueOf(config.getPort()));
		top.setBackground(Color.white);
		top.add(new JLabel("IP"));
		top.add(ipTextField);
		top.add(new JLabel("PORT"));
		top.add(portTextField);
		JButton reConnect = new JButton("Переподключиться");
		reConnect.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {

				config.setIp(ipTextField.getText());
				config.setPort(Integer.parseInt(portTextField.getText()));

				clientSocketConnect.destroy();
				clientSocketConnect = ClientSocketConnect.instance(config);
			}
		});
		top.add(reConnect);
		jPanelMain.add(top, bagConstraints);
		bagConstraints.weightx = 1;
		bagConstraints.gridy = 1;
		bagConstraints.weighty = 0.01;
		bagConstraints.anchor = GridBagConstraints.WEST;
		JPanel btns = new JPanel();
		btns.setSize(32, 32);
		btns.setBackground(Color.white);
		backBtn = new JButton(new ImageIcon(
				new ImageIcon("src/main/resources/back.png").getImage().getScaledInstance(32, 32, Image.SCALE_SMOOTH)));
		backBtn.setBackground(Color.white);
		btns.add(backBtn);
		btns.add(pathLabel);
		jPanelMain.add(btns, bagConstraints);
		bagConstraints.anchor = GridBagConstraints.CENTER;
		bagConstraints.gridy = 2;
		bagConstraints.weighty = 1;
		jPanelMain.add(contentArea, bagConstraints);
		frame.add(jPanelMain);

		backBtn.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				clientSocketConnect.sendMessageToServer(
						new Message(MessageInputType.Up, MessageOutputType.Success, currentFolder));
				generateLayout();
			}
		});

		jPanelMain.addMouseListener(new DoubleClickListener(null, new ContextMenu(currentFolder).setClientForm(this)));

		frame.setVisible(true);
		generateLayout();
	}

}
