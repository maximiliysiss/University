package serverremoter.service;

import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Фильтр для адреса
 * @author
 *
 */
public class CustomFilter {
	/**
	 * Для какого IP
	 */
	@JsonProperty("ip")
	private String ip;
	/**
	 * Путь до папки
	 */
	@JsonProperty("path")
	private String path;

	public String getIp() {
		return ip;
	}

	public void setIp(String ip) {
		this.ip = ip;
	}

	public String getPath() {
		return path;
	}

	public void setPath(String path) {
		this.path = path;
	}

}
