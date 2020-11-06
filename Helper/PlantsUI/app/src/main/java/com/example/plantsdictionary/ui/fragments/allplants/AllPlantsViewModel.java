package com.example.plantsdictionary.ui.fragments.allplants;

import android.os.Build;

import androidx.annotation.RequiresApi;
import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.plantsdictionary.data.models.Plants;
import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.interfaces.DataProvider;
import com.example.plantsdictionary.ui.controls.ui.models.FamilyPlantViewModel;
import com.example.plantsdictionary.ui.controls.ui.models.PlantViewModel;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Predicate;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class AllPlantsViewModel extends ViewModel {

    private MutableLiveData<List<PlantViewModel>> plantsViewModel;
    private MutableLiveData<String> searchValue;

    private List<Predicate<PlantViewModel>> filters = new ArrayList<>();

    public AllPlantsViewModel() {
        plantsViewModel = new MutableLiveData<>();
        searchValue = new MutableLiveData<>();
        reloadData();
    }

    public void clearFilters() {
        filters.clear();
    }

    public void addFilter(Predicate<PlantViewModel> plantViewModelPredicate) {
        filters.add(plantViewModelPredicate);
    }

    public void reloadData() {
        Stream<PlantViewModel> stream = IOContainer.getInstance().resolve(DataProvider.class).getAllPlants().stream().map(x -> new PlantViewModel(x));
        String searchText = searchValue.getValue();
        if (searchText != null && searchText.trim().length() != 0)
            stream = stream.filter(x -> x.getTitle().toLowerCase().contains(searchText.toLowerCase()));
        for (Predicate<PlantViewModel> predicate : filters)
            stream = stream.filter(predicate);
        plantsViewModel.postValue(stream.collect(Collectors.toList()));
    }

    public MutableLiveData<List<PlantViewModel>> getPlantsViewModel() {
        return plantsViewModel;
    }

    public MutableLiveData<String> getSearchValue() {
        return searchValue;
    }
}