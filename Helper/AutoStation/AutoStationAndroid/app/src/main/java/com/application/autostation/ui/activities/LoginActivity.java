package com.application.autostation.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Toast;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.app.UserContext.UserContext;
import com.application.autostation.network.callbacks.ActionCallback;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.LoginResult;
import com.application.autostation.network.models.output.LoginModel;

public class LoginActivity extends AppCompatActivity {

    EditText login;
    EditText password;
    public LinearLayout linearLayout;

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
            startActivity(new Intent(this, AdminActivity.class));
        } else {
            linearLayout.setVisibility(View.VISIBLE);
        }
    }

    public void login(View view) {
        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();

        if (loginString.length() == 0 || passwordString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthRetrofit().login(new LoginModel(loginString, passwordString)).enqueue(new UniversalCallback<>(getBaseContext(), loginResult -> {
            App.setUserContext(new UserContext(loginResult.getAccessToken(), loginResult.getRefreshToken()));
            startActivity(new Intent(LoginActivity.this, AdminActivity.class));
        }));
    }

    public void buyTicket(View view) {
        startActivity(new Intent(this, UserActivity.class));
    }
}
