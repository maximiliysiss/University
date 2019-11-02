package com.application.library.ui;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.application.library.R;
import com.application.library.app.App;
import com.application.library.app.UserContext.UserContext;
import com.application.library.network.callbacks.UniversalCallback;
import com.application.library.network.models.input.LoginResult;
import com.application.library.network.models.output.LoginModel;

/**
 * Форма входа
 */
public class LoginActivity extends AppCompatActivity {

    /**
     * Поля ввода
     */
    EditText login;
    EditText password;
    public LinearLayout linearLayout;


    /**
     * Нельзя надать кнопку назад
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
         * Попробуем авторизироваться
         */
        UserContext res = App.tryLogin();
        /**
         * Если окей, то просто откроем форму
         * иначе предлагаем ввести данные
         */
        if (res != null) {
            App.setUserContext(res);
            startActivity(new Intent(this, MainActivity.class));
        } else {
            linearLayout.setVisibility(View.VISIBLE);
        }
    }

    /**
     * Авторизация
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
     * Кнопка регистрация
     * @param view
     */
    public void register(View view) {
        startActivity(new Intent(this, RegisterActivity.class));
    }
}
