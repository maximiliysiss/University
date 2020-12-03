package com.server.data;

public class SqlInteractorFactory {

    private static SqlInteractor instance;

    public synchronized static SqlInteractor create(String connectionString) {

        if (instance != null)
            return instance;

        try {
            Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
            return instance = new MSSQLInteractor(connectionString);
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        }
        return null;
    }
}
