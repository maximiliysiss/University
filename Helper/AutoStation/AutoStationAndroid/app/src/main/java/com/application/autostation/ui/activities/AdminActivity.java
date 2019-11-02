package com.application.autostation.ui.activities;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.google.android.material.bottomnavigation.BottomNavigationView;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import java.io.Serializable;

public class AdminActivity extends AppCompatActivity {

    NavController navController;

    @Override
    public void onBackPressed() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this).setTitle("Сменить пользователя?")
                .setNegativeButton("Нет", (dialog, which) -> {
                })
                .setPositiveButton("Да", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        App.signOut();
                        startActivity(new Intent(AdminActivity.this, LoginActivity.class));
                    }
                });

        builder.create().show();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        BottomNavigationView navView = findViewById(R.id.nav_view);

        AppBarConfiguration appBarConfiguration = new AppBarConfiguration.Builder(
                R.id.navigation_schedules, R.id.navigation_statistics, R.id.navigation_points)
                .build();
        navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, appBarConfiguration);
        NavigationUI.setupWithNavController(navView, navController);
    }

    public void openFragment(int id) {
        navController.navigate(id);
    }

    public void openFragment(int id, String name, Serializable serializable) {
        Bundle bundle = new Bundle();
        bundle.putSerializable(name, serializable);
        navController.navigate(id, bundle);
    }

}