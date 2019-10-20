package com.school.android.ui.activity;

import android.os.Bundle;
import android.util.ArraySet;
import android.view.Menu;

import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.school.android.R;
import com.school.android.application.App;
import com.school.android.factory.MenuFactory;

import androidx.appcompat.app.AppCompatActivity;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import java.io.Serializable;
import java.util.Set;

public class MainActivity extends ActivityFragmenter {

    NavController navController;

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
        navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupWithNavController(navView, navController);
    }

}
