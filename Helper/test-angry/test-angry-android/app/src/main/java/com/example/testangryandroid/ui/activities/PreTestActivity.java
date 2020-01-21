package com.example.testangryandroid.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.testangryandroid.R;
import com.example.testangryandroid.app.App;
import com.example.testangryandroid.ui.extendings.EmptyAction;
import com.example.testangryandroid.ui.extendings.OnSafeClickEvent;

public class PreTestActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pre_test);

        Button start = findViewById(R.id.startTest);
        start.setOnClickListener(new OnSafeClickEvent(() -> {
            EditText name =findViewById(R.id.nameString);
            String nameString = name.getText().toString().trim();
            if(nameString.length()==0){
                Toast.makeText(getBaseContext(), "Введите имя", Toast.LENGTH_SHORT).show();
                return;
            }

            App.setUserName(nameString);
            startActivity(new Intent(PreTestActivity.this, TestActivity.class));
        }));
    }
}
