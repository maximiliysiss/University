package serverremoter.service;

import java.io.PrintWriter;
import java.io.StringWriter;

import javax.swing.JTextArea;

/*
 * Логгер в TextArea
 */
public class LoggerOnTextArea {
	/*
	 * Логгер
	 */
	private static LoggerOnTextArea area;

	/*
	 * Получить логгер
	 */
	public static LoggerOnTextArea instance() throws Exception {
		if (area == null)
			throw new Exception("Not found Logger");
		return area;
	}

	/*
	 * Получить логгер
	 */
	public static LoggerOnTextArea instance(JTextArea jTextArea) {
		if (area == null)
			area = new LoggerOnTextArea(jTextArea);
		return area;
	}

	/*
	 * Вывод
	 */
	private JTextArea jTextArea;

	/*
	 * Конструктор
	 */
	public LoggerOnTextArea(JTextArea jTextArea) {
		super();
		this.jTextArea = jTextArea;
	}

	/*
	 * Вывод информации
	 */
	public void info(String text) {
		jTextArea.append("info: " + text + "\n");
	}

	/*
	 * Вывод ошибки
	 */
	public void error(Exception text) {
		StringWriter sw = new StringWriter();
		PrintWriter pw = new PrintWriter(sw);
		text.printStackTrace(pw);
		jTextArea.append("error: " + text.getMessage() + "\n" + sw.toString() + "\n");
	}
}
