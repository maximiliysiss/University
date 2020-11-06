package com.example.plantsdictionary.ui.fragments.byfamily;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class ByFamilyViewModel extends ViewModel {

    private MutableLiveData<String> mText;

    public ByFamilyViewModel() {
        mText = new MutableLiveData<>();
        mText.setValue("This is slideshow fragment");
    }

    public LiveData<String> getText() {
        return mText;
    }
}