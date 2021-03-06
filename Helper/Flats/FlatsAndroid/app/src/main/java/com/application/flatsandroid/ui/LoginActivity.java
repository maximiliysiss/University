package com.application.flatsandroid.ui;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.application.flatsandroid.R;
import com.application.flatsandroid.app.App;
import com.application.flatsandroid.network.callbacks.UniversalCallback;
import com.application.flatsandroid.network.models.input.LoginResult;
import com.application.flatsandroid.network.models.output.LoginRegisterModel;

/**
 * Форма авторизации
 */
public class LoginActivity extends AppCompatActivity {

    /**
     * Поля
     */
    EditText login;
    EditText password;

    /**
     * Нельзя ходить назад
     */
    @Override
    public void onBackPressed() {
    }

    /**
     * Создание
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        login = findViewById(R.id.login);
        password = findViewById(R.id.password);
    }

    /**
     * Кнопка Зарегистрироваться
     * @param view
     */
    public void register(View view) {
        startActivity(new Intent(this, RegisterActivity.class));
    }

    /**
     * Кнопка входа
     * @param view
     */
    public void login(View view) {
        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();

        if (loginString.length() == 0 || passwordString.length() == 0) {
            Toast.makeText(getBaseContext(), "Неправильный логин/пароль", Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthService().login(new LoginRegisterModel(loginString, passwordString)).enqueue(new UniversalCallback<LoginResult>(getBaseContext(), x -> {
            App.setRole(x.getRole());
            startActivity(new Intent(this, MainActivity.class));
        }).setMessage("Неправильный логин/пароль"));
    }
}
