package com.school.android.ui.activity;

import android.os.Bundle;
import android.view.Menu;

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
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        BottomNavigationView navView = findViewById(R.id.nav_view);

        navView.inflateMenu(MenuFactory.getMenuFactory().create(App.getUserContext().getUser()));
        Menu menu = navView.getMenu();
        AppBarConfiguration appBarConfiguration = new AppBarConfiguration.Builder(menu).build();
        navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, appBarConfiguration);
        NavigationUI.setupWithNavController(navView, navController);
        openFragment(menu.getItem(0).getItemId());
    }

}
