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

public class LoginActivity extends AppCompatActivity {

    EditText login;
    EditText password;
    public LinearLayout linearLayout;


    @Override
    public void onBackPressed() {

    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        login = findViewById(R.id.login);
        password = findViewById(R.id.password);
        linearLayout = findViewById(R.id.linear);

        UserContext res = App.tryLogin();
        if (res != null) {
            App.setUserContext(res);
            startActivity(new Intent(this, MainActivity.class));
        } else {
            linearLayout.setVisibility(View.VISIBLE);
        }
    }

    public void login(View view) {

        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();

        if (loginString.length() == 0 || passwordString.length() == 0) {
            Toast.makeText(getBaseContext(), "Введите логин/пароль", Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthRetrofit().login(new LoginModel(loginString, passwordString)).enqueue(new UniversalCallback<LoginResult>(getBaseContext(), x -> {
            App.setUserContext(new UserContext(x.getAccessToken(), x.getRefreshToken()));
            startActivity(new Intent(this, MainActivity.class));
        }).setMessage("Неправильный логин/пароль"));
    }

    public void onlyShow(View view) {
        startActivity(new Intent(this, MainActivity.class));
    }
}
