package remoterclient.application;

import java.io.File;
import java.util.Scanner;

import com.fasterxml.jackson.databind.ObjectMapper;

import remoterclient.forms.ClientForm;
import serverremoter.service.Config;

public class StartClient {

	/**
	 * Запуск килента
	 * @param args
	 */
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

		new ClientForm(config);
	}

}
