package com.example.plantsdictionary;

import androidx.room.Room;

import com.example.plantsdictionary.data.logic.ComplexProvider;
import com.example.plantsdictionary.data.logic.JsonProvider;
import com.example.plantsdictionary.data.logic.LocalDatabase;
import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.infrastructure.ioc.ScopeType;
import com.example.plantsdictionary.interfaces.DataProvider;

public class Application extends android.app.Application {
    @Override
    public void onCreate() {
        super.onCreate();

        IOContainer ioContainer = IOContainer.getInstance();
        ioContainer.register(DataProvider.class, new ComplexProvider(new JsonProvider(this),
                Room.databaseBuilder(getApplicationContext(), LocalDatabase.class, "PlantsDB").build()), ScopeType.Scoped);
    }
}
