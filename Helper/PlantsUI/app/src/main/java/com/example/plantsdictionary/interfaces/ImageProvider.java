package com.example.plantsdictionary.interfaces;

import android.graphics.Bitmap;

/**
 * Провайдер изображений
 */
public interface ImageProvider {
    Bitmap loadBitmap(String name);
}
