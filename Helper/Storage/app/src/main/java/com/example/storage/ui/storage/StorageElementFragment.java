package com.example.storage.ui.storage;

import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.room.Room;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.Spinner;

import com.example.storage.R;
import com.example.storage.data.DataContext;
import com.example.storage.data.models.Group;
import com.example.storage.data.models.StoragePosition;
import com.example.storage.ui.ModelFragment;
import com.example.storage.ui.ObjectFragment;
import com.example.storage.ui.data.Spinner.GroupSpinnerAdapter;

/**
 * Фрагмент позиции
 */
public class StorageElementFragment extends ModelFragment<StoragePosition> {

    /**
     * Подключение к БД
     */
    DataContext dataContext;

    /**
     * Сумма
     */
    EditText sum;
    /**
     * Выпадающий список групп
     */
    Spinner group;

    public StorageElementFragment() {
        super(R.id.navigation_storage);
    }

    /**
     * Создание View
     * @param inflater
     * @param container
     * @param savedInstanceState
     * @return
     */
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_storage_element, container, false);
    }

    /**
     * Заполнение View
     */
    @Override
    public void onStart() {
        super.onStart();

        dataContext = Room.databaseBuilder(getContext(), DataContext.class, getString(R.string.database)).fallbackToDestructiveMigration().allowMainThreadQueries().build();

        setObj((StoragePosition) this.getArguments().getSerializable("Storage"));

        sum = getView().findViewById(R.id.sum);
        group = getView().findViewById(R.id.group);

        group.setAdapter(new GroupSpinnerAdapter(dataContext.getGroupDAO().getAllGroups(), R.layout.spinner_group, getContext()));

        if (getObj().getGroupId() > 0)
            group.setSelection(getIndexForSpinner(group, getObj().getGroupId()));
        sum.setText(String.valueOf(getObj().getSum()));

        this.generateView();
    }

    /**
     * Установить объект
     * @param obj
     * @return
     */
    @Override
    public ObjectFragment<StoragePosition> setObj(StoragePosition obj) {
        setEdit(obj.getId() != 0);
        return super.setObj(obj);
    }

    /**
     * Удаление
     */
    @Override
    protected void onDelete() {
        dataContext.getStoragePositionDAO().delete(getObj());
    }

    /**
     * Добавление
     */
    @Override
    protected void onAdd() {
        dataContext.getStoragePositionDAO().insertAll(getObj());
    }

    /**
     * Изменение
     */
    @Override
    protected void onEdit() {
        dataContext.getStoragePositionDAO().update(getObj());
    }

    /**
     * Выгрузить объект с View
     */
    @Override
    protected void loadObject() {
        getObj().setSum(Double.parseDouble(sum.getText().toString()));
        getObj().setGroupId(((Group) group.getSelectedItem()).getId());
    }

    /**
     * Получить индекс элемент из выпадающего списка
     * @param spinner
     * @param group
     * @return
     */
    protected int getIndexForSpinner(Spinner spinner, int group) {
        for (int i = 0; i < spinner.getCount(); i++) {
            if (((Group) spinner.getItemAtPosition(i)).getId() == group)
                return i;
        }
        return 0;
    }
}
