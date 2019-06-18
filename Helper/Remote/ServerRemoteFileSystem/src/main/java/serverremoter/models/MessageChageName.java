package serverremoter.models;

import java.io.Serializable;

import serverremoter.enums.MessageInputType;
import serverremoter.enums.MessageOutputType;

/**
 * Сообщение на изменение названия
 * Просто к Message добавляется еще newName полей и все
 * @author
 *
 */
public class MessageChageName extends Message implements Serializable {

	protected String newName;

	public String getNewName() {
		return newName;
	}

	public void setNewName(String newName) {
		this.newName = newName;
	}

	public MessageChageName() {
	}

	public MessageChageName(MessageInputType inputType, MessageOutputType outputType, String name) {
		super(inputType, outputType);
		this.newName = name;
	}

	public MessageChageName(MessageInputType inputType, MessageOutputType outputType, String name,
			FileSystemNodeRemote fileSystemNodeRemote) {
		super(inputType, outputType, fileSystemNodeRemote);
		this.newName = name;
	}

	public MessageChageName(String newName) {
		super();
		this.newName = newName;
		this.inputType = MessageInputType.Rename;
	}

}
