package com.example.plantsdictionary.ui.fragments.home;

import android.os.Build;

import androidx.annotation.RequiresApi;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.plantsdictionary.infrastructure.ioc.IOContainer;
import com.example.plantsdictionary.interfaces.DataProvider;
import com.example.plantsdictionary.ui.controls.ui.models.ActionViewModel;

import java.util.List;
import java.util.stream.Collectors;

public class HomeViewModel extends ViewModel {

    private MutableLiveData<List<ActionViewModel>> actionViewModels;

    @RequiresApi(api = Build.VERSION_CODES.N)
    public HomeViewModel() {
        actionViewModels = new MutableLiveData<>();
        actionViewModels.postValue(IOContainer.getInstance().resolve(DataProvider.class).getAllActions().stream()
                .map(x -> new ActionViewModel(x)).collect(Collectors.toList()));
    }

    public MutableLiveData<List<ActionViewModel>> getActionViewModels() {
        return actionViewModels;
    }
}