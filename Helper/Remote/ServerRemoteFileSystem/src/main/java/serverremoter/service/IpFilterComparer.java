package serverremoter.service;

/**
 * Сравнение адресов
 * @author
 *
 */
public class IpFilterComparer {
	public static boolean Compare(String f, String s) {
		String[] fOctet = f.split("\\.");
		String[] sOctet = s.split("\\.");
		if (fOctet.length != sOctet.length)
			return false;

		for (int i = 0; i < fOctet.length; i++) {
			if (fOctet[i] == "*" || sOctet[i] == "*") {
				continue;
			}
			if (!fOctet[i].equals(sOctet[i]))
				return false;
		}
		return true;
	}
}
