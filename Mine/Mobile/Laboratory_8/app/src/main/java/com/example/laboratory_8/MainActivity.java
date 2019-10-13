package com.example.laboratory_8;

import android.Manifest;
import android.app.Activity;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.content.res.Configuration;
import android.hardware.Camera;
import android.os.Bundle;
import android.os.Environment;
import android.provider.ContactsContract;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.Toast;

import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;

public class MainActivity extends Activity implements SurfaceHolder.Callback, View.OnClickListener, Camera.PictureCallback, Camera.PreviewCallback, Camera.AutoFocusCallback {
    private static final int MY_PERMISSIONS_REQUEST_CAMERA = 42;
    private Camera camera;
    private SurfaceHolder surfaceHolder;
    private SurfaceView preview;

    private void getAccess(String type, int code) {
        if (ContextCompat.checkSelfPermission(this, type) != PackageManager.PERMISSION_GRANTED) {
            if (ActivityCompat.shouldShowRequestPermissionRationale(this, type)) {
            } else {
                ActivityCompat.requestPermissions(this, new String[]{type}, code);
            }
        } else {
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);


        getAccess(Manifest.permission.CAMERA, MY_PERMISSIONS_REQUEST_CAMERA);
        getAccess(Manifest.permission.WRITE_EXTERNAL_STORAGE, 43);

        getWindow().addFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN);


        requestWindowFeature(Window.FEATURE_NO_TITLE);

        setContentView(R.layout.activity_main);
        
        preview = findViewById(R.id.SurfaceView01);
        preview.setOnClickListener(this);

        surfaceHolder = preview.getHolder();
        surfaceHolder.addCallback(this);
        surfaceHolder.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);
    }

    @Override
    protected void onResume() {
        super.onResume();
        try {
            if (camera == null)
                camera = Camera.open();
        } catch (Exception ex) {
            Toast.makeText(getApplicationContext(), ex.getMessage(), Toast.LENGTH_SHORT).show();
        }
    }

    @Override
    protected void onPause() {
        super.onPause();

        if (camera != null) {
            camera.setPreviewCallback(null);
            camera.stopPreview();
            camera.release();
            camera = null;
        }
    }

    @Override
    public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) {
    }

    @Override
    public void surfaceCreated(SurfaceHolder holder) {
        try {
            if (camera == null)
                camera = Camera.open();
            camera.setPreviewDisplay(holder);
            camera.setPreviewCallback(this);
        } catch (IOException e) {
            e.printStackTrace();
        }

        Camera.Size previewSize = camera.getParameters().getPreviewSize();
        float aspect = (float) previewSize.width / previewSize.height;

        int previewSurfaceWidth = preview.getWidth();
        int previewSurfaceHeight = preview.getHeight();

        ViewGroup.LayoutParams lp = preview.getLayoutParams();

        camera.setDisplayOrientation(90);
        lp.height = previewSurfaceHeight;
        lp.width = (int) (previewSurfaceHeight / aspect);

        preview.setLayoutParams(lp);
        camera.startPreview();
    }

    @Override
    public void surfaceDestroyed(SurfaceHolder holder) {
    }

    private boolean takePictureAvailable = true;

    @Override
    public void onClick(View v) {
        if (v == preview && takePictureAvailable) {
            camera.takePicture(null, null, null, this);
            takePictureAvailable = false;
        }
    }

    @Override
    public void onPictureTaken(byte[] paramArrayOfByte, Camera paramCamera) {
        try {
            String path = Environment.getExternalStorageDirectory() + "/CameraExample/";
            File saveDir = new File(path);

            if (!saveDir.exists()) {
                saveDir.mkdirs();
            }

            FileOutputStream os = new FileOutputStream(String.format(path + "%d.jpg", System.currentTimeMillis()));
            os.write(paramArrayOfByte);
            os.close();
            Toast.makeText(getApplicationContext(), "Photo saved", Toast.LENGTH_SHORT).show();
        } catch (Exception e) {
            Toast.makeText(getApplicationContext(), e.getMessage(), Toast.LENGTH_SHORT).show();
        }

        paramCamera.startPreview();
        takePictureAvailable = true;
    }

    @Override
    public void onAutoFocus(boolean paramBoolean, Camera paramCamera) {
        if (paramBoolean) {
            paramCamera.takePicture(null, null, null, this);
        }
    }

    @Override
    public void onPreviewFrame(byte[] paramArrayOfByte, Camera paramCamera) {
    }
}
