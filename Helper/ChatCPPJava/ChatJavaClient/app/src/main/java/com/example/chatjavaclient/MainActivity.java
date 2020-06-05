package com.example.chatjavaclient;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    EditText nicknameEditText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        nicknameEditText = findViewById(R.id.name);
    }

    public void openChat(View view) {

        String nickname = nicknameEditText.getText().toString().trim();

        if (nickname.length() == 0) {
            Toast.makeText(getBaseContext(), "Enter nickname", Toast.LENGTH_SHORT).show();
            return;
        }
        Intent intent = new Intent(this, ChatActivity.class);
        intent.putExtra("userName", nickname);
        startActivity(intent);
    }
}
