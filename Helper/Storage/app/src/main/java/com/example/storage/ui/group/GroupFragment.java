package com.example.storage.ui.group;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.room.Room;

import com.example.storage.MainActivity;
import com.example.storage.R;
import com.example.storage.data.DataContext;
import com.example.storage.data.models.Group;
import com.example.storage.ui.data.RecyclerCardViewAdapter;
import com.example.storage.ui.data.ViewHolder.GroupViewHolder;

/**
 * Фрагмент для групп
 */
public class GroupFragment extends Fragment {

    /**
     * Поключение к БД
     */
    DataContext dataContext;
    /**
     * Вывод списка групп
     */
    RecyclerView recyclerView;

    /**
     * Создание View
     * @param inflater
     * @param container
     * @param savedInstanceState
     * @return
     */
    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_group, container, false);
    }

    /**
     * Старт и задание данных
     */
    @Override
    public void onStart() {
        super.onStart();

        getView().findViewById(R.id.add).setOnClickListener(v -> {
            ((MainActivity)getActivity()).openFragment(R.id.navigation_group_element, new Group(), "Group");
        });

        dataContext = Room.databaseBuilder(getContext(), DataContext.class, getString(R.string.database)).allowMainThreadQueries().fallbackToDestructiveMigration().build();
        recyclerView = getView().findViewById(R.id.groups);
        recyclerView.setLayoutManager(new LinearLayoutManager(this.getContext()));
        recyclerView.setAdapter(new RecyclerCardViewAdapter(dataContext.getGroupDAO().getAllGroups(), this, R.layout.group_recycler,
                x -> new GroupViewHolder(x)));
    }
}