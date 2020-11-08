package com.example.plantsdictionary;

import android.os.Bundle;
import android.os.Parcelable;
import android.view.View;
import android.view.Menu;

import com.example.plantsdictionary.ui.interfaces.ActivityNavigator;
import com.example.plantsdictionary.ui.interfaces.ToolbarActivity;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.android.material.snackbar.Snackbar;
import com.google.android.material.navigation.NavigationView;

import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

/**
 * Главный Activity
 */
public class MainActivity extends AppCompatActivity implements ActivityNavigator, ToolbarActivity {


    private AppBarConfiguration mAppBarConfiguration;
    /**
     * Контроллер навигации
     */
    private NavController navController;
    /**
     * Тулбар
     */
    private Toolbar toolbar;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        NavigationView navigationView = findViewById(R.id.nav_view);
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        mAppBarConfiguration = new AppBarConfiguration.Builder(
                R.id.nav_home, R.id.nav_all_plants, R.id.nav_by_family, R.id.nav_favorite)
                .setDrawerLayout(drawer)
                .build();

        navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, mAppBarConfiguration);
        NavigationUI.setupWithNavController(navigationView, navController);
    }

    @Override
    public boolean onSupportNavigateUp() {
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        return NavigationUI.navigateUp(navController, mAppBarConfiguration)
                || super.onSupportNavigateUp();
    }

    /**
     * Открыть фрагмент
     *
     * @param layoutId
     */
    @Override
    public void navigateTo(int layoutId) {
        navController.navigate(layoutId);
    }

    /**
     * Открыть фрагмент с аргументами
     *
     * @param layoutId
     * @param id
     * @param parcelable
     */
    @Override
    public void navigateTo(int layoutId, int id, Parcelable parcelable) {
        Bundle bundle = new Bundle();
        if (parcelable != null && id > 0)
            bundle.putParcelable(getString(id), parcelable);
        navController.navigate(layoutId, bundle);
    }

    /**
     * Обновить заголовок
     *
     * @param title
     */
    @Override
    public void updateTitle(String title) {
        toolbar.setTitle(title);
    }

    /**
     * Получить заголовок
     *
     * @return
     */
    @Override
    public String getToolbarTitle() {
        return toolbar.getTitle().toString();
    }


}