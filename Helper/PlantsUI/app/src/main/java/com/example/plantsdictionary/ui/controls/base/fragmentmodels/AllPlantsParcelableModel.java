package com.example.plantsdictionary.ui.controls.base.fragmentmodels;

import android.content.Context;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.data.models.actions.ActionArguments;
import com.example.plantsdictionary.interfaces.models.IModelActionSerialize;

import java.util.List;
import java.util.Optional;

/**
 * Модель для фрагмента с отображением растений
 */
public class AllPlantsParcelableModel implements Parcelable, IModelActionSerialize {

    /**
     * Только избранное
     */
    private boolean favorites;
    /**
     * Какую семью
     */
    private String family;

    public boolean getFavorites() {
        return favorites;
    }

    public String getFamily() {
        return family;
    }

    public AllPlantsParcelableModel(boolean favorites, String family) {
        this.favorites = favorites;
        this.family = family;
    }

    public AllPlantsParcelableModel() {
    }

    /**
     * Конструктор с парсером
     *
     * @param in
     */
    protected AllPlantsParcelableModel(Parcel in) {
        family = in.readString();
        boolean[] booleans = new boolean[1];
        in.readBooleanArray(booleans);
        favorites = booleans[0];
    }

    /**
     * Default создатель
     */
    public static final Creator<AllPlantsParcelableModel> CREATOR = new Creator<AllPlantsParcelableModel>() {
        @Override
        public AllPlantsParcelableModel createFromParcel(Parcel in) {
            return new AllPlantsParcelableModel(in);
        }

        @Override
        public AllPlantsParcelableModel[] newArray(int size) {
            return new AllPlantsParcelableModel[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    /**
     * Запись в Parcel
     *
     * @param dest
     * @param flags
     */
    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(family);
        dest.writeBooleanArray(new boolean[]{favorites});
    }

    /**
     * Загрузка из листа аргументов
     *
     * @param argumentsList
     */
    @Override
    public void load(List<ActionArguments> argumentsList) {

        Optional<ActionArguments> familyArg = argumentsList.stream().filter(x -> "family".equals(x.getName().toLowerCase())).findFirst();
        Optional<ActionArguments> favoritesArg = argumentsList.stream().filter(x -> "favorites".equals(x.getName().toLowerCase())).findFirst();

        if (familyArg.isPresent())
            family = familyArg.get().getValue();
        if (favoritesArg.isPresent())
            favorites = Boolean.valueOf(favoritesArg.get().getValue());
    }

    /**
     * Загрузка из контекста фрагмента. Немного hardCode
     *
     * @param context
     * @param bundle
     */
    @Override
    public void load(Context context, Bundle bundle) {
        if (bundle.containsKey(context.getString(R.string.favorites_key))) {
            favorites = bundle.getBoolean(context.getString(R.string.favorites_key));
        }
    }
}
