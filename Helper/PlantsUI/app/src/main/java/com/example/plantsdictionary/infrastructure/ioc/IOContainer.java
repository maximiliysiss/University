package com.example.plantsdictionary.infrastructure.ioc;

import android.os.Build;

import androidx.annotation.RequiresApi;

import java.util.HashMap;
import java.util.Map;

public class IOContainer implements IContainer {
    private static IOContainer ioContainer;

    private IOContainer() {
    }

    public static synchronized IOContainer getInstance() {
        if (ioContainer != null)
            return ioContainer;
        return ioContainer = new IOContainer();
    }

    private Map<Class, Object> memoryHardContainer = new HashMap<>();

    @RequiresApi(api = Build.VERSION_CODES.N)
    public <T> T resolve(Class<T> type) {
        return (T) memoryHardContainer.get(type);
    }

    @Override
    public <I, T> void register(Class<I> i, T obj, ScopeType scopeType) {
        memoryHardContainer.put(i, obj);
    }
}
