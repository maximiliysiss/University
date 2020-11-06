package com.example.plantsdictionary.ui.fragments.allplants;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class AllPlantsViewModel extends ViewModel {

    private MutableLiveData<String> mText;

    public AllPlantsViewModel() {
        mText = new MutableLiveData<>();
        mText.setValue("This is gallery fragment");
    }

    public LiveData<String> getText() {
        return mText;
    }
}