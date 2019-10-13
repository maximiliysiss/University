package com.example.laboratory_3;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

public class SecondActivity extends AppCompatActivity {

    TextView textView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second);

        textView = findViewById(R.id.hello);

        Bundle bundle = this.getIntent().getExtras();
        if(bundle != null){
            String name = bundle.getString("name");
            if(name==null || name.trim().length() == 0)
                name = "404";
            textView.append(name);
        }

    }

    public void toThirdActivity(View view) {
        startActivity(new Intent(this, ThirdActivity.class));
    }
}
