package com.example.plantsdictionary.ui.fragments.allplants;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.annotation.NonNull;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.ui.controls.base.FragmentWithBundleLoadModel;
import com.example.plantsdictionary.ui.controls.base.fragmentmodels.AllPlantsParcelableModel;
import com.example.plantsdictionary.ui.controls.bindings.TextBinder;
import com.example.plantsdictionary.ui.controls.recyclerview.RecyclerCardViewAdapter;
import com.example.plantsdictionary.ui.controls.ui.PlantRecyclerViewHolder;
import com.example.plantsdictionary.ui.interfaces.ToolbarActivity;

/**
 * Фрагмент списка растений
 */
public class AllPlantsFragment extends FragmentWithBundleLoadModel<AllPlantsParcelableModel> {

    /**
     * ViewModel
     */
    private AllPlantsViewModel allPlantsViewModel;

    public AllPlantsFragment() {
        super(R.string.allplantsparcelablemodel, AllPlantsParcelableModel.class);
    }

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        allPlantsViewModel = new ViewModelProvider(this).get(AllPlantsViewModel.class);
        View root = inflater.inflate(R.layout.fragment_all_plants, container, false);

        // Компонент отображения списка
        RecyclerView recyclerView = root.findViewById(R.id.plants);
        // Отображаем 2мя колонками
        recyclerView.setLayoutManager(new GridLayoutManager(getContext(), 2, GridLayoutManager.VERTICAL, false));
        // Установим привязку к данным
        recyclerView.setAdapter(new RecyclerCardViewAdapter(allPlantsViewModel.getPlantsViewModel(), this, R.layout.plant_item,
                x -> new PlantRecyclerViewHolder(x)));

        // Поиск
        EditText searchEditText = root.findViewById(R.id.search);
        searchEditText.addTextChangedListener(new TextBinder(allPlantsViewModel.getSearchValue()));

        // При изменении поиска и списка должны происходить изменения компонентов
        allPlantsViewModel.getSearchValue().observe(getViewLifecycleOwner(), x -> allPlantsViewModel.reloadData());
        allPlantsViewModel.getPlantsViewModel().observe(getViewLifecycleOwner(), x -> recyclerView.getAdapter().notifyDataSetChanged());
        allPlantsViewModel.getTitleAppender().observe(getViewLifecycleOwner(), x -> updateTitle(x));

        allPlantsViewModel.loadModel(getContext(), model);

        return root;
    }

    private void updateTitle(String titleAppender) {
        ToolbarActivity toolbarActivity = (ToolbarActivity) getActivity();
        toolbarActivity.updateTitle(toolbarActivity.getToolbarTitle() + " (" + titleAppender + ")");
    }
}