package com.example.laboratory_4_task;

import androidx.appcompat.app.AppCompatActivity;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.provider.Settings;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity implements SensorEventListener {


    SensorManager sensorManager;
    Sensor light;
    TextView valueText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        sensorManager = (SensorManager) getSystemService(SENSOR_SERVICE);
        light = sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT);
        valueText = findViewById(R.id.textValue);
    }

    @Override
    protected void onStart() {
        super.onStart();
        sensorManager.registerListener(this, light, sensorManager.SENSOR_DELAY_FASTEST);
    }

    @Override
    protected void onStop() {
        super.onStop();
        sensorManager.unregisterListener(this, light);
    }

    @Override
    public void onSensorChanged(SensorEvent sensorEvent) {
        if(sensorEvent.sensor.getType()==Sensor.TYPE_LIGHT){
            valueText.setText(String.valueOf(sensorEvent.values[0]));
            Settings.System.putInt(this.getContentResolver(), Settings.System.SCREEN_BRIGHTNESS, Math.round(sensorEvent.values[0]*255));
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int i) {

    }
}
