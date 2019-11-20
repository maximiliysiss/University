package com.application.carrepairandroid.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.application.carrepairandroid.R;
import com.application.carrepairandroid.application.App;
import com.application.carrepairandroid.application.UserContext.UserContext;
import com.application.carrepairandroid.network.callbacks.UniversalCallback;
import com.application.carrepairandroid.network.models.output.LoginModel;

/**
 * Форма входа
 */
public class LoginActivity extends AppCompatActivity {

    /**
     * Поля ввода
     */
    EditText loginTextView;
    EditText passwordTextView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        loginTextView = findViewById(R.id.login);
        passwordTextView = findViewById(R.id.password);
    }

    /**
     * Нельзя листать назад
     */
    @Override
    public void onBackPressed() {
    }

    /**
     * Кнопка входа
     * @param view
     */
    public void login(View view) {
        String login = loginTextView.getText().toString().trim();
        String password = passwordTextView.getText().toString().trim();

        if (login.length() == 0 || password.length() == 0) {
            Toast.makeText(this, getString(R.string.not_login_password), Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthRetrofit().login(new LoginModel(login, password)).enqueue(new UniversalCallback<>(this,
                object -> {
                    App.setUserContext(new UserContext(object.getAccessToken(),object.getRefreshToken()));
                    startActivity(new Intent(LoginActivity.this, MainActivity.class));
                }));
    }

    /**
     * Просто просмотр
     * @param view
     */
    public void toNext(View view) {
        startActivity(new Intent(this, MainActivity.class));
    }
}
