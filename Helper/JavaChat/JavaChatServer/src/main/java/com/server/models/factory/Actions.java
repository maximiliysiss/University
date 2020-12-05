package com.server.models.factory;

import com.server.models.ActionMessage;

public class Actions {
    public static ActionMessage FailLogin = new ActionMessage("loginfail","Login/Password is incorrect");
    public static ActionMessage ServerIsFull = new ActionMessage("loginfail", "Server is full");
}
