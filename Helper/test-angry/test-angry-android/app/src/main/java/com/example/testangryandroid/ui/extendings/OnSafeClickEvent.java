package com.example.testangryandroid.ui.extendings;

import android.os.SystemClock;
import android.view.View;

public class OnSafeClickEvent implements View.OnClickListener {

    EmptyAction emptyAction;

    public OnSafeClickEvent(EmptyAction emptyAction) {
        this.emptyAction = emptyAction;
    }

    long lastClickTime;

    @Override
    public void onClick(View v) {
        if (SystemClock.elapsedRealtime() - lastClickTime < 500) {
            return;
        }

        lastClickTime = SystemClock.elapsedRealtime();

        emptyAction.action();
    }
}
