package com.example.plantsdictionary.infrastructure.ioc;

public class IOCFactory {

    private static IContainer iContainer;

    public synchronized static IContainer getIContainer() {
        if (iContainer != null)
            return iContainer;
        return iContainer = new IOContainer();
    }

}
