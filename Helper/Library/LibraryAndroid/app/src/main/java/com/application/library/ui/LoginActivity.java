package com.application.library.ui;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Toast;

import com.application.library.app.App;
import com.application.library.app.UserContext.UserContext;
import com.application.library.network.callbacks.UniversalCallback;
import com.application.library.network.models.input.LoginResult;
import com.application.library.network.models.output.LoginModel;
import com.school.library.R;

/**
 * Форма логина
 */
public class LoginActivity extends AppCompatActivity {

    /**
     * Поля
     */
    EditText login;
    EditText password;
    /**
     * Контейнер для полей
     */
    public LinearLayout linearLayout;


    /**
     * Назад нельзя листать
     */
    @Override
    public void onBackPressed() {

    }

    /**
     * Создание формы
     *
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        login = findViewById(R.id.login);
        password = findViewById(R.id.password);
        linearLayout = findViewById(R.id.linear);

        /**
         * Попытка авторизоваться сразу
         */
        UserContext res = App.tryLogin();
        if (res != null) {
            App.setUserContext(res);
            startActivity(new Intent(this, MainActivity.class));
        } else {
            linearLayout.setVisibility(View.VISIBLE);
        }
    }

    /**
     * Кнопка логин
     *
     * @param view
     */
    public void login(View view) {

        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();

        if (loginString.length() == 0 || passwordString.length() == 0) {
            Toast.makeText(getBaseContext(), "Введите логин/пароль", Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthRetrofit().login(new LoginModel(loginString, passwordString)).enqueue(new UniversalCallback<LoginResult>(getBaseContext(), x -> {
            App.setUserContext(new UserContext(x.getAccessToken(), x.getRefreshToken(), x.getUserRole()));
            startActivity(new Intent(this, MainActivity.class));
        }).setMessage("Неправильный логин/пароль"));
    }

    /**
     * Кнопка зарегистрироваться
     *
     * @param view
     */
    public void register(View view) {
        startActivity(new Intent(this, RegisterActivity.class));
    }
}
