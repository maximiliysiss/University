package com.example.plantsdictionary.ui.fragments.allplants;

import android.content.Context;
import android.os.Build;

import androidx.annotation.RequiresApi;
import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.data.models.Plants;
import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.interfaces.DataProvider;
import com.example.plantsdictionary.ui.controls.base.fragmentmodels.AllPlantsParcelableModel;
import com.example.plantsdictionary.ui.controls.ui.models.FamilyPlantViewModel;
import com.example.plantsdictionary.ui.controls.ui.models.PlantViewModel;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;
import java.util.function.Predicate;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class AllPlantsViewModel extends ViewModel {

    private MutableLiveData<String> titleAppender;
    private MutableLiveData<List<PlantViewModel>> plantsViewModel;
    private MutableLiveData<String> searchValue;
    private DataProvider dataProvider = IOContainer.getInstance().resolve(DataProvider.class);

    private List<Predicate<PlantViewModel>> filters = new ArrayList<>();


    public AllPlantsViewModel() {
        plantsViewModel = new MutableLiveData<>();
        searchValue = new MutableLiveData<>();
        titleAppender = new MutableLiveData<>();
    }

    public void clearFilters() {
        filters.clear();
    }

    public void addFilter(Predicate<PlantViewModel> plantViewModelPredicate) {
        filters.add(plantViewModelPredicate);
    }

    public void reloadData() {
        Stream<PlantViewModel> stream = IOContainer.getInstance().resolve(DataProvider.class).getAllPlants().stream()
                .map(plants -> new PlantViewModel(this, plants, dataProvider.isFavoritesExists(plants.getId())));
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

    public MutableLiveData<String> getTitleAppender() {
        return titleAppender;
    }

    public void loadModel(Context context, AllPlantsParcelableModel model) {
        // Очистим фильтры
        clearFilters();

        if (model != null) {
            // Добавим фильтр по семье
            if (model.getFamily() != null) {
                String family = model.getFamily();
                addFilter(x -> x.getFamily().equals(family));
                titleAppender.setValue(family);
            }

            // Только избранное
            if (model.getFavorites()) {
                addFilter(x -> x.isFavorite());
                titleAppender.setValue(context.getString(R.string.favorite_title));
            }
        }

        reloadData();
    }
}