package com.example.plantsdictionary.data.logic;

import android.content.Context;
import android.content.res.AssetManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.interfaces.ImageProvider;

import java.io.IOException;
import java.io.InputStream;

/**
 * Провайдер изображений из asset
 */
public class LocalImageProvider implements ImageProvider {

    private Context context;

    public LocalImageProvider(Context context) {
        this.context = context;
    }

    /**
     * Загрузка изображения из asset
     *
     * @param name
     * @return
     */
    @Override
    public Bitmap loadBitmap(String name) {
        AssetManager assetManager = context.getAssets();

        Bitmap bitmap = null;
        try {
            InputStream inputStream = assetManager.open(context.getString(R.string.images_path) + name);
            bitmap = BitmapFactory.decodeStream(inputStream);
        } catch (IOException e) {
        }

        return bitmap;
    }
}
