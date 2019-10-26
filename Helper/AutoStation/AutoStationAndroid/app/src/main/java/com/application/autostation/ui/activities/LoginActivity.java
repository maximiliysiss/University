package com.application.autostation.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.app.UserContext.UserContext;

public class LoginActivity extends AppCompatActivity {

    EditText login;
    EditText password;
    public LinearLayout linearLayout;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
    }

    public void login(View view) {
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

    public void buyTicket(View view) {
        startActivity(new Intent(this, UserActivity.class));
    }
}
