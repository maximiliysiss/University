package com.application.library.ui;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.application.library.R;
import com.application.library.app.App;
import com.application.library.app.UserContext.UserContext;
import com.application.library.network.callbacks.UniversalCallback;
import com.application.library.network.models.output.LoginModel;

/**
 * Форма регистрации
 */
public class RegisterActivity extends AppCompatActivity {

    /**
     * Создание формы
     *
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
    }

    /**
     * Нажать кнопку зарегистрироваться
     *
     * @param view
     */
    public void register(View view) {
        EditText login = findViewById(R.id.login);
        EditText password = findViewById(R.id.password);
        EditText repeat = findViewById(R.id.repeat);

        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();
        String repeatString = repeat.getText().toString().trim();

        /**
         * Если что-то не заполнили
         */
        if (loginString.length() == 0 || passwordString.length() == 0 || repeatString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            return;
        }

        /**
         * Если пароли не совпадают
         */
        if (!passwordString.equals(repeatString)) {
            Toast.makeText(getBaseContext(), "Пароли не совпадают", Toast.LENGTH_SHORT).show();
            return;
        }

        /**
         * Регистрация в сервере
         */
        App.getAuthRetrofit().register(new LoginModel(loginString, passwordString)).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            App.setUserContext(new UserContext(x.getAccessToken(), x.getRefreshToken(), x.getUserRole()));
            startActivity(new Intent(this, MainActivity.class));
        }));
    }
}
