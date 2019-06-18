package serverremoter.service;

import java.util.ArrayList;

import com.fasterxml.jackson.annotation.JsonProperty;

/*
 * Конфигурвция
 */
public class Config {

	/*
	 * IP
	 */
	@JsonProperty("ip")
	private String ip;
	/*
	 * Port
	 */
	@JsonProperty("port")
	private int port;
	/*
	 * Максимальное количество игроков
	 */
	@JsonProperty("maximum")
	private int maximum;
	/*
	 * Путь до виртуальной папке
	 */
	@JsonProperty("path")
	private String path;

	@JsonProperty("filters")
	private ArrayList<CustomFilter> filters = new ArrayList<CustomFilter>();

	public ArrayList<CustomFilter> getFilters() {
		return filters;
	}

	public void setFilters(ArrayList<CustomFilter> filters) {
		this.filters = filters;
	}

	public String getPath() {
		return path;
	}

	public void setPath(String path) {
		this.path = path;
	}

	public int getMaximum() {
		return maximum;
	}

	public void setMaximum(int maximum) {
		this.maximum = maximum;
	}

	public String getIp() {
		return ip;
	}

	public void setIp(String ip) {
		this.ip = ip;
	}

	public int getPort() {
		return port;
	}

	public void setPort(int port) {
		this.port = port;
	}

}
