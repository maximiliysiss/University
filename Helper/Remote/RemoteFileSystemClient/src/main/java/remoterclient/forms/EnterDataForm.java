package remoterclient.forms;

import javax.swing.JFrame;

import remoterclient.listener.ContextClickHandler;
import remoterclient.listener.TextInputHandler;

import javax.swing.JTextField;
import javax.swing.JButton;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

/**
 * Класс формы для ввода данных
 * @author
 *
 */
public class EnterDataForm {

	/**
	 * Элементы формы
	 */
	JFrame frame = new JFrame();
	TextInputHandler clickHandler;
	private JTextField textField;

	/**
	 * Конструктор
	 * @param clickHandler
	 */
	public EnterDataForm(TextInputHandler clickHandler) {
		this.clickHandler = clickHandler;
		frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		frame.getContentPane().setLayout(null);
		frame.setSize(452, 115);

		textField = new JTextField();
		textField.setBounds(10, 11, 414, 20);
		frame.getContentPane().add(textField);
		textField.setColumns(10);

		JButton btnNewButton = new JButton("Изменить");
		btnNewButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				String text = textField.getText().trim();
				frame.dispose();
				if (text.length() > 0) {
					clickHandler.onInputText(text);
				}
			}
		});
		btnNewButton.setBounds(295, 42, 129, 23);
		frame.getContentPane().add(btnNewButton);
		frame.setVisible(true);
	}
}
