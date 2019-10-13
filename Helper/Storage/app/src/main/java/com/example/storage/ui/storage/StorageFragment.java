package com.example.storage.ui.storage;

import android.os.Bundle;
import android.view.ContextMenu;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ExpandableListView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.RecyclerView;
import androidx.room.Room;

import com.example.storage.MainActivity;
import com.example.storage.R;
import com.example.storage.data.DataContext;
import com.example.storage.data.models.StorageInGroups;
import com.example.storage.data.models.StoragePosition;
import com.example.storage.ui.data.ExpandableList.ExpandableListConstructor;
import com.example.storage.ui.data.ExpandableList.StoragePositionExpandable;
import com.example.storage.ui.data.ExpandableListAdapter;
import com.example.storage.ui.data.RecyclerCardViewAdapter;
import com.example.storage.ui.data.ViewHolder.GroupViewHolder;

import java.util.HashMap;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Фрагмент позиций
 */
public class StorageFragment extends Fragment {

    /**
     * Константы для элементов меню
     */
    private static final int MENU_DELETE = 42;
    private static final int MENU_EDIT = 43;
    /**
     * Подключение к БД
     */
    DataContext dataContext;
    /**
     * Выпадающий список
     */
    ExpandableListView expandableListView;
    /**
     * Кнопка добавить
     */
    Button add;

    /**
     * Данные
     */
    HashMap<String, List<StoragePosition>> hashMap;

    /**
     * Создание View
     *
     * @param inflater
     * @param container
     * @param savedInstanceState
     * @return
     */
    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_storage, container, false);
    }

    /**
     * Заполнение View
     */
    @Override
    public void onStart() {
        super.onStart();
        dataContext = Room.databaseBuilder(getContext(), DataContext.class, getString(R.string.database)).fallbackToDestructiveMigration().allowMainThreadQueries().build();

        expandableListView = getView().findViewById(R.id.storages);
        List<StorageInGroups> groupsList = dataContext.getGroupWithStoragesDAO().getAllGroups().stream().filter(x -> x.getStoragePositionList().size() > 0).collect(Collectors.toList());
        hashMap = new HashMap<>();
        groupsList.forEach(x -> {
            hashMap.put(x.getGroup().getName(), x.getStoragePositionList());
        });

        add = getView().findViewById(R.id.add);
        add.setVisibility(dataContext.getGroupDAO().getAllGroups().size() > 0 ? View.VISIBLE : View.INVISIBLE);
        add.setOnClickListener(v -> {
            ((MainActivity) getActivity()).openFragment(R.id.navigation_storage_element, new StoragePosition(), "Storage");
        });

        /**
         * Добавим контекстное меню к элементу
         */
        expandableListView.setOnCreateContextMenuListener((menu, v, menuInfo) -> {
            ExpandableListView.ExpandableListContextMenuInfo info =
                    (ExpandableListView.ExpandableListContextMenuInfo) menuInfo;
            int type = ExpandableListView.getPackedPositionType(info.packedPosition);
            if (type == ExpandableListView.PACKED_POSITION_TYPE_CHILD) {
                menu.add(0, MENU_EDIT, 0, "Изменить");
                menu.add(0, MENU_DELETE, 0, "Удалить");
            }
        });
        expandableListView.setAdapter(new ExpandableListAdapter<>(getContext(), new StoragePositionExpandable(hashMap)));
    }

    /**
     * Обработка выбора ContextMenu
     *
     * @param item
     * @return
     */
    @Override
    public boolean onContextItemSelected(MenuItem item) {

        ExpandableListView.ExpandableListContextMenuInfo info =
                (ExpandableListView.ExpandableListContextMenuInfo) item.getMenuInfo();

        int type = ExpandableListView.getPackedPositionType(info.packedPosition);
        if (type == ExpandableListView.PACKED_POSITION_TYPE_CHILD) {
            int groupPos = ExpandableListView.getPackedPositionGroup(info.packedPosition);
            int childPos = ExpandableListView.getPackedPositionChild(info.packedPosition);

            switch (item.getItemId()) {
                case MENU_DELETE:
                    dataContext.getStoragePositionDAO().delete(hashMap.get(hashMap.keySet().stream().collect(Collectors.toList()).get(groupPos)).get(childPos));
                    ((MainActivity) getActivity()).openFragment(R.id.navigation_storage);
                    return true;
                case MENU_EDIT:
                    ((MainActivity) getActivity()).openFragment(R.id.navigation_storage_element, hashMap.get(hashMap.keySet().stream().collect(Collectors.toList()).get(groupPos)).get(childPos),
                            "Storage");
                    return true;
            }
        }

        return super.onContextItemSelected(item);
    }
}