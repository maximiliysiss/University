package com.example.plantsdictionary.ui.fragments.byfamily;

import android.os.Build;

import androidx.annotation.RequiresApi;
import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.interfaces.DataProvider;
import com.example.plantsdictionary.ui.controls.ui.models.FamilyPlantViewModel;

import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class ByFamilyViewModel extends ViewModel {

    private MutableLiveData<List<FamilyPlantViewModel>> familyViewModel;
    private MutableLiveData<String> searchValue;

    public ByFamilyViewModel() {
        familyViewModel = new MutableLiveData<>();
        searchValue = new MutableLiveData<>();
        reloadData();
    }

    public void reloadData() {
        Stream<FamilyPlantViewModel> stream = IOContainer.getInstance().resolve(DataProvider.class).getFamilyPlants().stream().map(x -> new FamilyPlantViewModel(x));
        String searchText = searchValue.getValue();
        if (searchText != null && searchText.trim().length() != 0)
            stream = stream.filter(x -> x.getTitle().toLowerCase().contains(searchText.toLowerCase()));
        familyViewModel.postValue(stream.collect(Collectors.toList()));
    }

    public MutableLiveData<String> getSearchValue() {
        return searchValue;
    }

    public MutableLiveData<List<FamilyPlantViewModel>> getFamilyViewModel() {
        return familyViewModel;
    }
}