package com.example.plantsdictionary.ui.fragments.home;

import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.plantsdictionary.infrastructure.ioc.IOCFactory;
import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.interfaces.DataProvider;
import com.example.plantsdictionary.ui.controls.ui.models.ActionViewModel;

import java.util.List;
import java.util.stream.Collectors;

/**
 * Модель действий
 */
public class HomeViewModel extends ViewModel {

    private MutableLiveData<List<ActionViewModel>> actionViewModels;

    public HomeViewModel() {
        actionViewModels = new MutableLiveData<>();
        actionViewModels.postValue(IOCFactory.getIContainer().resolve(DataProvider.class).getAllActions().stream()
                .map(x -> new ActionViewModel(x)).collect(Collectors.toList()));
    }

    public MutableLiveData<List<ActionViewModel>> getActionViewModels() {
        return actionViewModels;
    }
}