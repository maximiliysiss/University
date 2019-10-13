package com.example.laboratory_4;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.hardware.Sensor;
import android.hardware.SensorDirectChannel;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.widget.TextView;

public class MainActivity extends Activity  implements SensorEventListener {

    TextView ax_x;
    TextView ax_y;
    TextView ax_z;
    TextView m_x;
    TextView m_y;
    TextView m_z;
    TextView near;
    TextView light;

    SensorManager sensorManager;
    Sensor ax;
    Sensor m;
    Sensor l;
    Sensor distance;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ax_x = findViewById(R.id.ax_x);
        ax_y = findViewById(R.id.ax_y);
        ax_z = findViewById(R.id.ax_z);
        m_x = findViewById(R.id.m_x);
        m_y = findViewById(R.id.m_y);
        m_z = findViewById(R.id.m_z);
        light = findViewById(R.id.light);
        near = findViewById(R.id.near);

        sensorManager = (SensorManager) getSystemService(SENSOR_SERVICE);
        ax = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        m = sensorManager.getDefaultSensor(Sensor.TYPE_MAGNETIC_FIELD);
        distance = sensorManager.getDefaultSensor(Sensor.TYPE_PROXIMITY);
        l = sensorManager.getDefaultSensor(Sensor.TYPE_LIGHT);
    }

    @Override
    public void onStart(){
        super.onStart();
        sensorManager.registerListener(this, ax, sensorManager.SENSOR_DELAY_FASTEST);
        sensorManager.registerListener(this, m, sensorManager.SENSOR_DELAY_FASTEST);
        sensorManager.registerListener(this, l, sensorManager.SENSOR_DELAY_FASTEST);
        sensorManager.registerListener(this, distance, sensorManager.SENSOR_DELAY_FASTEST);
    }

    @Override
    public void onStop(){
        super.onStop();
        sensorManager.unregisterListener(this, ax);
        sensorManager.unregisterListener(this, m);
        sensorManager.unregisterListener(this, l);
        sensorManager.unregisterListener(this, distance);
    }

    @Override
    public void onSensorChanged(SensorEvent sensorEvent) {
        switch(sensorEvent.sensor.getType()){
            case Sensor.TYPE_ACCELEROMETER:
                ax_x.setText("X " + sensorEvent.values[0]);
                ax_y.setText("Y " + sensorEvent.values[1]);
                ax_z.setText("Z " + sensorEvent.values[2]);
                break;
            case Sensor.TYPE_MAGNETIC_FIELD:
                m_x.setText("X "+sensorEvent.values[0]);
                m_y.setText("Y "+sensorEvent.values[1]);
                m_z.setText("Z "+sensorEvent.values[2]);
                break;
            case Sensor.TYPE_PROXIMITY:
                near.setText("Приближение "+sensorEvent.values[0]);
                break;
            case Sensor.TYPE_LIGHT:
                light.setText("Освещеность "+sensorEvent.values[0]);
                break;
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int i) {

    }
}
