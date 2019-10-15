package com.school.android.ui.activity;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.application.UserContext;
import com.school.android.models.network.input.LoginResult;
import com.school.android.models.network.output.LoginModel;
import com.school.android.network.classes.CallbackAction;
import com.school.android.network.classes.UniversalCallback;

public class LoginActivity extends AppCompatActivity {

    EditText loginTextView;
    EditText passwordTextView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        loginTextView = findViewById(R.id.login);
        passwordTextView = findViewById(R.id.password);
    }

    @Override
    public void onBackPressed() {
    }

    public void login(View view) {
        String login = loginTextView.getText().toString().trim();
        String password = passwordTextView.getText().toString().trim();

        if (login.length() == 0 || password.length() == 0) {
            Toast.makeText(this, getString(R.string.not_login_password), Toast.LENGTH_SHORT).show();
            return;
        }

        App.getAuthRetrofit().login(new LoginModel(login, password)).enqueue(new UniversalCallback<>(this,
                new CallbackAction<LoginResult>() {
                    @Override
                    public void process(LoginResult object) {
                        App.setUserContext(new UserContext(object.getRefreshToken(), object.getAccessToken(),
                                object.getUserType(), object.getId()));
                        startActivity(new Intent(LoginActivity.this, MainActivity.class));
                    }
                }));
    }
}
