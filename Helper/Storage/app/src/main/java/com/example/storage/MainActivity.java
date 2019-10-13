package com.example.storage;

import android.os.Bundle;
import android.os.Parcelable;
import android.view.View;
import android.widget.FrameLayout;

import com.example.storage.data.models.Group;
import com.example.storage.ui.group.GroupElementFragment;
import com.google.android.material.bottomnavigation.BottomNavigationView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import java.io.Serializable;

/**
 * Главное окно
 */
public class MainActivity extends AppCompatActivity {

    /*Менеджер фрагментов*/
    FragmentManager myFragmentManager;
    /*То, куда отрисовывать фрагмент*/
    FrameLayout frameLayout;
    NavController navController;

    /**
     * Создание окна
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        BottomNavigationView navView = findViewById(R.id.nav_view);
        AppBarConfiguration appBarConfiguration = new AppBarConfiguration.Builder(
                R.id.statistics_home, R.id.navigation_group, R.id.navigation_storage)
                .build();
        navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, appBarConfiguration);
        NavigationUI.setupWithNavController(navView, navController);

        frameLayout = findViewById(R.id.nav_host_fragment);
        myFragmentManager = getSupportFragmentManager();
    }

    /*Открыть фрагмент*/
    public void openFragment(int id, Serializable serializable, String name) {
        Bundle bundle = new Bundle();
        bundle.putSerializable(name, serializable);
        navController.navigate(id, bundle);
    }

    /**
     * Открыть фрагмент
     * @param id
     */
    public void openFragment(int id) {
        navController.navigate(id);
    }
}
