package com.server.data;

public class MSSQLInteractor implements SqlInteractor {

    private String connectionString;

    public MSSQLInteractor(String connectionString) {
        this.connectionString = connectionString;
    }
}
