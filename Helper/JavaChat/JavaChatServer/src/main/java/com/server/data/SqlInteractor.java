package com.server.data;

public interface SqlInteractor {
    Integer tryLogin(String login, String password);
    Integer tryRegister(String login, String password);
}
