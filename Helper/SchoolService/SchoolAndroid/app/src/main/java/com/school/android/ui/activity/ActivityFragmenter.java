package com.school.android.ui.activity;

import android.app.Activity;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;
import androidx.navigation.NavController;

import com.school.android.R;

import java.io.Serializable;

public abstract class ActivityFragmenter extends AppCompatActivity {

    protected NavController navController;


    public void openFragment(int id) {
        navController.navigate(id);
    }

    public void openFragment(int id, String name, Serializable serializable) {
        Bundle bundle = new Bundle();
        bundle.putSerializable(name, serializable);
        navController.navigate(id, bundle);
    }

    public void openFragment(int id, Bundle bundle){
        navController.navigate(id, bundle);
    }

    public void openFragment(int id, String name, Serializable serializable, Bundle bundle) {
        bundle.putSerializable(name, serializable);
        navController.navigate(id, bundle);
    }

}
