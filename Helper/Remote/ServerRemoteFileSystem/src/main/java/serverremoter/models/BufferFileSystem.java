package serverremoter.models;

import serverremoter.enums.BufferType;

/**
 * Бефер под действия
 * @author
 *
 */
public class BufferFileSystem {
	/**
	 * Какой элемент
	 */
	private FileSystemNodeRemote fileSystemNodeRemote;
	/**
	 * Какой буфер
	 */
	private BufferType bufferType;

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

	public BufferType getBufferType() {
		return bufferType;
	}

	public void setBufferType(BufferType bufferType) {
		this.bufferType = bufferType;
	}

	/**
	 * КОнстурктор
	 * @param fileSystemNodeRemote
	 * @param bufferType
	 */
	public BufferFileSystem(FileSystemNodeRemote fileSystemNodeRemote, BufferType bufferType) {
		super();
		this.fileSystemNodeRemote = fileSystemNodeRemote;
		this.bufferType = bufferType;
	}

}
