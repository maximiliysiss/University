package serverremoter.application;

import java.io.File;
import java.util.Scanner;

import com.fasterxml.jackson.databind.ObjectMapper;

import serverremoter.forms.ServerForm;
import serverremoter.service.Config;

/**
 * Запуск сервера
 * @author
 *
 */
public class Start {

	public static void main(String[] args) {
		Config config = null;
		try (Scanner fileRead = new Scanner(new File("configuration\\configuration.config"))) {
			
			String configString = fileRead.nextLine();
			System.out.println(configString);
			ObjectMapper mapper = new ObjectMapper();
			config = mapper.readValue(configString, Config.class);
		} catch (Exception ex) {
			System.out.println(ex.getMessage());
			return;
		}

		new ServerForm(config);
	}

}
