package com.example.testangryandroid.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.testangryandroid.R;
import com.example.testangryandroid.app.App;
import com.example.testangryandroid.app.UserContext.DatabaseContext;
import com.example.testangryandroid.app.UserContext.UserContext;
import com.example.testangryandroid.network.callbacks.ActionCallback;
import com.example.testangryandroid.network.callbacks.UniversalCallback;
import com.example.testangryandroid.network.models.LoginModel;
import com.example.testangryandroid.network.models.LoginResult;
import com.example.testangryandroid.ui.extendings.EmptyAction;
import com.example.testangryandroid.ui.extendings.OnSafeClickEvent;

/**
 * Activity with textBox
 */
public class PreTestActivity extends AppCompatActivity {

    EditText login;
    EditText password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pre_test);

        login = findViewById(R.id.login);
        password = findViewById(R.id.password);
    }

    public void login(View view) {
        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();

        if (loginString.length() == 0 || passwordString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните форму", Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthRetrofit().login(new LoginModel(loginString, passwordString)).enqueue(new UniversalCallback<LoginResult>(getBaseContext(), loginResult -> {
            if (loginResult != null) {
                DatabaseContext.setUserContext(new UserContext(loginResult));
                startActivity(new Intent(PreTestActivity.this, MainActivity.class));
            }
        }).setMessage("Неправильный логин/пароль"));
    }

    public void register(View view) {
        startActivity(new Intent(this, RegisterActivity.class));
    }
}
