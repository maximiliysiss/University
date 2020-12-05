package com.server.data;

import com.server.common.Cryptographic;
import com.server.models.Message;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

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
        String passwordHash = Cryptographic.get().md5(password);

        try (Connection conn = getConnection();
             PreparedStatement statement = conn.prepareStatement("select Id from Users where Login = ? and PasswordHash = ?")) {
            statement.setString(1, login);
            statement.setString(2, passwordHash);
            try (ResultSet resultSet = statement.executeQuery()) {
                boolean isExists = resultSet.next();
                if (!isExists)
                    return null;
                return resultSet.getInt("Id");
            }
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return null;
    }

    public boolean isExists(String login) {
        try (Connection conn = getConnection();
             PreparedStatement statement = conn.prepareStatement("select count(1) c from Users where Login = ?")) {
            statement.setString(1, login);
            try (ResultSet resultSet = statement.executeQuery()) {
                if (resultSet.next())
                    return resultSet.getInt("c") > 0;
            }
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return true;
    }

    @Override
    public Integer tryRegister(String login, String password) {
        if (isExists(login))
            return null;

        String passwordHash = Cryptographic.get().md5(password);
        try (Connection conn = getConnection();
             PreparedStatement statement = conn.prepareStatement("insert into Users(Login,PasswordHash) values(?,?)")) {
            statement.setString(1, login);
            statement.setString(2, passwordHash);
            statement.execute();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return tryLogin(login, password);
    }

    @Override
    public Integer getUserByName(String name) {
        try (Connection conn = getConnection();
             PreparedStatement statement = conn.prepareStatement("select Id from Users where Login = ?")) {
            statement.setString(1, name);
            try (ResultSet resultSet = statement.executeQuery()) {
                if (resultSet.next())
                    return resultSet.getInt("Id");
            }
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return null;
    }

    @Override
    public void insertMessage(Message message) {
        try (Connection conn = getConnection();
             PreparedStatement statement = conn.prepareStatement("insert into Messages(UserId, Content) values (?,?)")) {
            statement.setInt(1, message.getSenderId());
            statement.setString(2, message.getMessage());
            statement.execute();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
    }

    @Override
    public void insertPrivateMessage(Message message, Integer userId) {
        try (Connection conn = getConnection();
             PreparedStatement statement = conn.prepareStatement("execute sp_create_privatemessage @userId = ?, @content = ?, @receiveId = ?")) {
            statement.setInt(1, message.getSenderId());
            statement.setString(2, message.getMessage());
            statement.setInt(3, userId);
            statement.execute();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
    }

    @Override
    public void clearData() {
        try (Connection conn = getConnection();
             PreparedStatement statement = conn.prepareStatement("delete from private_message; delete from Messages;")) {
            statement.execute();
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
    }

    @Override
    public List<String> loadMessages(int id) {
        List<String> strings = new ArrayList<>();
        try (Connection conn = getConnection();
             PreparedStatement statement = conn.prepareStatement("exec sp_messages @id = 0, @userId = ?")) {
            statement.setInt(1, id);
            try (ResultSet resultSet = statement.executeQuery()) {
                while (resultSet.next()) {
                    strings.add(resultSet.getString("Content"));
                }
            }
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return strings;
    }
}
