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
 * Форма регистрация
 */
public class RegisterActivity extends AppCompatActivity {

    /**
     * Поля
     */
    EditText login;
    EditText password;
    EditText confirm;

    /**
     * Создание
     *
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        login = findViewById(R.id.login);
        password = findViewById(R.id.password);
        confirm = findViewById(R.id.confirm);
    }

    /**
     * Кнопка регистрации
     *
     * @param view
     */
    public void register(View view) {
        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();
        String confirmString = confirm.getText().toString().trim();

        if (loginString.length() == 0 || passwordString.length() == 0 || confirmString.length() == 0) {
            Toast.makeText(getBaseContext(), "Введите данные", Toast.LENGTH_SHORT).show();
            return;
        }

        if (!passwordString.equals(confirmString)) {
            Toast.makeText(getBaseContext(), "Пароли не совпадают", Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthService().register(new LoginRegisterModel(loginString, passwordString)).enqueue(
                new UniversalCallback<LoginResult>(getBaseContext(), x -> {
                    App.setRole(x.getRole());
                    startActivity(new Intent(this, MainActivity.class));
                }).setMessage("Такой пользователь уже существует"));
    }
}
