package com.school.android.ui.activity;

import android.os.Bundle;

import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.school.android.R;
import com.school.android.application.App;
import com.school.android.factory.MenuFactory;

public class MainActivity extends ActivityFragmenter {

    @Override
    public void onBackPressed() {
        super.onBackPressed();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        BottomNavigationView navView = findViewById(R.id.nav_view);

        navView.inflateMenu(MenuFactory.getMenuFactory().create(App.getUserContext().getUser()));
        AppBarConfiguration appBarConfiguration = new AppBarConfiguration.Builder(navView.getMenu()).build();
        navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, appBarConfiguration);
        NavigationUI.setupWithNavController(navView, navController);
    }

}
