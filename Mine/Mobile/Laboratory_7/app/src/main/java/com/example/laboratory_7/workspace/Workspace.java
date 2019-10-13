package com.example.laboratory_7.workspace;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.util.ArraySet;
import android.util.AttributeSet;
import android.util.Pair;
import android.view.MotionEvent;
import android.view.View;

import androidx.annotation.Nullable;

import java.util.Set;

public class Workspace extends View {

    Paint paint;
    int size = 5;


    Path actualPath;
    Set<Path> paths = new ArraySet<>();

    private void init() {
        paint = new Paint();
        paint.setColor(Color.BLACK);
        paint.setStrokeWidth(size);
        paint.setStyle(Paint.Style.STROKE);
    }

    public Workspace(Context context) {
        super(context);
        init();
    }

    public Workspace(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        init();
    }

    public Workspace(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
        init();
    }

    public Workspace(Context context, @Nullable AttributeSet attrs, int defStyleAttr, int defStyleRes) {
        super(context, attrs, defStyleAttr, defStyleRes);
        init();
    }

    public void handleMouse(MotionEvent motionEvent) {
        switch (motionEvent.getAction()) {
            case MotionEvent.ACTION_UP:
                paths.add(actualPath);
                break;
            case MotionEvent.ACTION_DOWN:
                actualPath = new Path();
                actualPath.moveTo(motionEvent.getX(), motionEvent.getY());
                break;
            case MotionEvent.ACTION_MOVE:
                actualPath.lineTo(motionEvent.getX(), motionEvent.getY());
                break;
        }
        invalidate();
    }

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        paths.forEach(path -> {
            canvas.drawPath(path, paint);
        });
        if (actualPath != null)
            canvas.drawPath(actualPath, paint);
    }
}
