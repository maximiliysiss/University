package main.java.common;

/**
 * Контекст пользовательского контекста
 */
public class UserContext {

    private static String login;
    private static int userId;

    public static String getLogin() {
        return login;
    }

    public static int getUserId() {
        return userId;
    }

    public static void setUser(String login, int id) {
        UserContext.login = login;
        UserContext.userId = id;
    }
}
