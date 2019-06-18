package serverremoter.models;

import java.io.Serializable;

import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;

/**
 * Сообщение
 * @author
 *
 */
public class Message implements Serializable {
	/**
	 * Запрос клиента
	 */
	protected MessageInputType inputType;
	/**
	 * Ответ сервера
	 */
	protected MessageOutputType outputType;
	/**
	 * С каким узлом работаем
	 */
	protected FileSystemNodeRemote fileSystemNodeRemote;

	/**
	 * Установка и полкчение полей + Конструкторы
	 * @return
	 */
	public FileSystemNodeRemote getFileSystemNodeRemote() {
		return fileSystemNodeRemote;
	}

	public void setFileSystemNodeRemote(FileSystemNodeRemote fileSystemNodeRemote) {
		this.fileSystemNodeRemote = fileSystemNodeRemote;
	}

	public MessageInputType getInputType() {
		return inputType;
	}

	public void setInputType(MessageInputType inputType) {
		this.inputType = inputType;
	}

	public MessageOutputType getOutputType() {
		return outputType;
	}

	public void setOutputType(MessageOutputType outputType) {
		this.outputType = outputType;
	}

	public Message(MessageInputType inputType, MessageOutputType outputType) {
		super();
		this.inputType = inputType;
		this.outputType = outputType;
	}

	public Message(MessageInputType inputType, MessageOutputType outputType,
			FileSystemNodeRemote fileSystemNodeRemote) {
		super();
		this.inputType = inputType;
		this.outputType = outputType;
		this.fileSystemNodeRemote = fileSystemNodeRemote;
	}

	public Message() {
		super();
	}

}
