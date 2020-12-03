package main.java.common;

public class UserContext {

    private static String login;
    private static int userId;

    public static String getLogin() {
        return login;
    }

    public static void setLogin(String login) {
        UserContext.login = login;
    }

    public static int getUserId() {
        return userId;
    }

    public static void setUserId(int userId) {
        UserContext.userId = userId;
    }
}
