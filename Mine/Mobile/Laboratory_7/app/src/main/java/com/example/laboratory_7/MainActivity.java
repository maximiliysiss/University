package com.example.laboratory_7;

import androidx.appcompat.app.AppCompatActivity;

import android.graphics.Paint;
import android.graphics.Path;
import android.os.Bundle;
import android.util.Log;
import android.util.Pair;
import android.view.MotionEvent;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.example.laboratory_7.workspace.Workspace;

public class MainActivity extends AppCompatActivity implements View.OnTouchListener {


    Workspace workspace;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        workspace = findViewById(R.id.workspace);
        workspace.setOnTouchListener(this);
    }

    @Override
    public boolean onTouch(View view, MotionEvent motionEvent) {
        workspace.handleMouse(motionEvent);
        return true;
    }
}
