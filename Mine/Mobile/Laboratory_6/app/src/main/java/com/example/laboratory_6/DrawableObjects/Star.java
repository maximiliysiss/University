package com.example.laboratory_6.DrawableObjects;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.util.AttributeSet;
import android.view.View;

import androidx.annotation.Nullable;

public class Star extends View{


    Paint paint;
    Path path;

    public Star(Context context) {
        super(context);
        init();
    }

    public void init(){
        paint = new Paint();
        paint.setColor(Color.BLACK);
        paint.setStyle(Paint.Style.STROKE);
        path = new Path();
    }

    public Star(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        init();
    }

    public Star(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
        init();
    }

    public Star(Context context, @Nullable AttributeSet attrs, int defStyleAttr, int defStyleRes) {
        super(context, attrs, defStyleAttr, defStyleRes);
        init();
    }

    @Override
    protected void onDraw(Canvas canvas) {
        float mid = getWidth() / 2;
        float min = Math.min(getWidth(), getHeight());
        float half = min / 2;
        mid = mid - half;

        paint.setStrokeWidth(0.2f);
        paint.setStyle(Paint.Style.STROKE);

        path.reset();

        paint.setStyle(Paint.Style.FILL);


        path.moveTo(mid + half * 0.5f, half * 0.84f);
        path.lineTo(mid + half * 1.5f, half * 0.84f);
        path.lineTo(mid + half * 0.68f, half * 1.45f);
        path.lineTo(mid + half * 1.0f, half * 0.5f);
        path.lineTo(mid + half * 1.32f, half * 1.45f);
        path.lineTo(mid + half * 0.5f, half * 0.84f);

        path.close();
        canvas.drawPath(path, paint);

        super.onDraw(canvas);
    }
}
