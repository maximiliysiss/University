package com.example.plantsdictionary.ui.interfaces;

import android.os.Bundle;

public interface ActivityNavigator {
    void navigateTo(int layoutId);
    <T> void navigateTo(int layoutId, Bundle bundle);
}
