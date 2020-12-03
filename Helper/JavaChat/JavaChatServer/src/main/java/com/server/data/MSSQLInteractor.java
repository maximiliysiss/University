package com.server.data;

import com.server.models.Message;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class MSSQLInteractor implements SqlInteractor {

    private String connectionString;

    public MSSQLInteractor(String connectionString) {
        this.connectionString = connectionString;
    }

    private Connection getConnection() {
        try {
            return DriverManager.getConnection(connectionString);
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return null;
    }

    @Override
    public Integer tryLogin(String login, String password) {
        try (Connection conn = getConnection()) {

        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return null;
    }

    @Override
    public Integer tryRegister(String login, String password) {
        return null;
    }

    @Override
    public Integer getUserByName(String name) {
        return null;
    }

    @Override
    public void insertMessage(Message message) {

    }

    @Override
    public void insertPrivateMessage(Message message, Integer userId) {

    }
}
