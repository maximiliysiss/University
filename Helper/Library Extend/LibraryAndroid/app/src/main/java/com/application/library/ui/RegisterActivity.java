package com.application.library.ui;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.application.library.app.App;
import com.application.library.app.UserContext.UserContext;
import com.application.library.network.callbacks.UniversalCallback;
import com.application.library.network.models.input.LoginResult;
import com.application.library.network.models.output.LoginModel;
import com.school.library.R;

public class RegisterActivity extends AppCompatActivity {

    EditText login;
    EditText password;
    EditText repeate;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        login = findViewById(R.id.login);
        password = findViewById(R.id.password);
        repeate = findViewById(R.id.repeate);
    }

    public void register(View view) {

        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();
        String repeateString = repeate.getText().toString().trim();

        if (loginString.length() == 0 || passwordString.length() == 0 || repeateString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT);
            return;
        }

        if (!passwordString.equals(repeateString)) {
            Toast.makeText(getBaseContext(), "Пароли не совпадают", Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthRetrofit().register(new LoginModel(loginString, passwordString)).enqueue(new UniversalCallback<LoginResult>(getBaseContext(), x -> {
            App.setUserContext(new UserContext(x.getAccessToken(), x.getRefreshToken(), x.getUserRole()));
            startActivity(new Intent(this, MainActivity.class));
        }).setMessage("Такой пользователь уже существует"));

    }
}
