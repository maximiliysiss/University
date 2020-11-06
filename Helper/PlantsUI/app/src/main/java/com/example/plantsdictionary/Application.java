package com.example.plantsdictionary;

import com.example.plantsdictionary.data.logic.XmlProvider;
import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.infrastructure.ioc.ScopeType;
import com.example.plantsdictionary.interfaces.DataProvider;

public class Application extends android.app.Application {
    @Override
    public void onCreate() {
        super.onCreate();

        IOContainer ioContainer = IOContainer.getInstance();
        ioContainer.register(DataProvider.class, new XmlProvider(this), ScopeType.Scoped);
    }
}
