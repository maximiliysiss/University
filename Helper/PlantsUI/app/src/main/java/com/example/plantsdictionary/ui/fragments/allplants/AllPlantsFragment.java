package com.example.plantsdictionary.ui.fragments.allplants;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.ui.controls.bindings.TextBinder;
import com.example.plantsdictionary.ui.controls.recyclerview.RecyclerCardViewAdapter;
import com.example.plantsdictionary.ui.controls.ui.PlantRecyclerViewHolder;
import com.example.plantsdictionary.ui.interfaces.ToolbarActivity;

public class AllPlantsFragment extends Fragment {

    private AllPlantsViewModel allPlantsViewModel;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        allPlantsViewModel = new ViewModelProvider(this).get(AllPlantsViewModel.class);
        View root = inflater.inflate(R.layout.fragment_all_plants, container, false);

        RecyclerView recyclerView = root.findViewById(R.id.plants);
        recyclerView.setLayoutManager(new GridLayoutManager(getContext(), 2, GridLayoutManager.VERTICAL, false));
        recyclerView.setAdapter(new RecyclerCardViewAdapter(allPlantsViewModel.getPlantsViewModel(), this, R.layout.plant_item,
                x -> new PlantRecyclerViewHolder(x)));

        EditText searchEditText = root.findViewById(R.id.search);
        searchEditText.addTextChangedListener(new TextBinder(allPlantsViewModel.getSearchValue()));

        allPlantsViewModel.getSearchValue().observe(getViewLifecycleOwner(), x -> allPlantsViewModel.reloadData());
        allPlantsViewModel.getPlantsViewModel().observe(getViewLifecycleOwner(), x -> recyclerView.getAdapter().notifyDataSetChanged());

        allPlantsViewModel.clearFilters();

        Bundle arguments = getArguments();
        if (arguments != null && arguments.containsKey(getString(R.string.family_key))) {
            String family = arguments.getString(getString(R.string.family_key));
            allPlantsViewModel.addFilter(x -> x.getFamily().equals(family));
            updateTitle(family);
        }

        return root;
    }

    private void updateTitle(String family) {
        ToolbarActivity toolbarActivity = (ToolbarActivity) getActivity();
        toolbarActivity.updateTitle(toolbarActivity.getToolbatTitle() + " (" + family + ")");
    }
}