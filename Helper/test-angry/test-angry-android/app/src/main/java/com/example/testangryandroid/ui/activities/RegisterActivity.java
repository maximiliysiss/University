package com.example.testangryandroid.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.example.testangryandroid.R;
import com.example.testangryandroid.app.App;
import com.example.testangryandroid.app.UserContext.DatabaseContext;
import com.example.testangryandroid.app.UserContext.UserContext;
import com.example.testangryandroid.network.callbacks.ActionCallback;
import com.example.testangryandroid.network.callbacks.UniversalCallback;
import com.example.testangryandroid.network.models.LoginResult;
import com.example.testangryandroid.network.models.RegisterModel;

public class RegisterActivity extends AppCompatActivity {

    EditText login;
    EditText password;
    EditText confirmPassword;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        login = findViewById(R.id.login);
        password = findViewById(R.id.password);
        confirmPassword = findViewById(R.id.confirmPassword);
    }

    public void register(View view) {
        String loginString = login.getText().toString().trim();
        String passwordString = password.getText().toString().trim();
        String confirmPasswordString = confirmPassword.getText().toString().trim();

        if (loginString.length() == 0 || passwordString.length() == 0 || confirmPasswordString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните форму", Toast.LENGTH_SHORT);
            return;
        }

        if (!passwordString.equals(confirmPasswordString)) {
            Toast.makeText(getBaseContext(), "Пароли не совпадают", Toast.LENGTH_SHORT);
            return;
        }

        App.getAuthRetrofit().register(new RegisterModel(loginString, passwordString)).enqueue(new UniversalCallback<>(getBaseContext(),
                loginResult -> {
                    DatabaseContext.setUserContext(new UserContext(loginResult));
                    startActivity(new Intent(RegisterActivity.this, MainActivity.class));
                }));
    }
}
