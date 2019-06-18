package serverremoter.models;

import java.io.Serializable;

/**
 * Размер сообщения (для передачи файлов)
 * @author
 *
 */
public class MessageSize implements Serializable {
	/*
	 * Сколько
	 */
	private int value;

	public MessageSize(int value) {
		super();
		this.value = value;
	}

	public int getValue() {
		return value;
	}

	public void setValue(int value) {
		this.value = value;
	}

}
