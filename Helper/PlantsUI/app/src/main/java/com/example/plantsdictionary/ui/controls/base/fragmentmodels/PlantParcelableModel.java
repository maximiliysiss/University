package com.example.plantsdictionary.ui.controls.base.fragmentmodels;

import android.os.Parcel;
import android.os.Parcelable;

/**
 * Модель для фрагмента отображения растения
 */
public class PlantParcelableModel implements Parcelable {

    /**
     * Id того, что отображаем
     */
    private int id;

    public int getId() {
        return id;
    }

    public PlantParcelableModel(int id) {
        this.id = id;
    }

    protected PlantParcelableModel(Parcel in) {
        id = in.readInt();
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeInt(id);
    }

    @Override
    public int describeContents() {
        return 0;
    }

    public static final Creator<PlantParcelableModel> CREATOR = new Creator<PlantParcelableModel>() {
        @Override
        public PlantParcelableModel createFromParcel(Parcel in) {
            return new PlantParcelableModel(in);
        }

        @Override
        public PlantParcelableModel[] newArray(int size) {
            return new PlantParcelableModel[size];
        }
    };
}
