package com.example.storage.ui.group;

import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.room.Room;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import com.example.storage.MainActivity;
import com.example.storage.R;
import com.example.storage.data.DataContext;
import com.example.storage.data.models.Group;
import com.example.storage.ui.ModelFragment;
import com.example.storage.ui.ObjectFragment;

/**
 * Фрагмент для группы
 */
public class GroupElementFragment extends ModelFragment<Group> {

    /**
     * Название
     */
    EditText editText;

    /**
     * Подключение к БД
     */
    DataContext dataContext;

    public GroupElementFragment() {
        super(R.id.navigation_group);
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
        dataContext = Room.databaseBuilder(getContext(), DataContext.class, getString(R.string.database)).fallbackToDestructiveMigration().allowMainThreadQueries().build();
        return inflater.inflate(R.layout.fragment_group_element, container, false);
    }

    /**
     * Старт фрагмента
     * Задаем внешний вид и данные
     */
    @Override
    public void onStart() {
        super.onStart();
        setObj((Group) getArguments().getSerializable("Group"));
        editText = getView().findViewById(R.id.name);
        editText.setText(getObj().getName());
        this.generateView();
    }

    /**
     * Удаление
     */
    @Override
    protected void onDelete() {
        dataContext.getGroupDAO().delete(getObj());
    }

    /**
     * Добавление
     */
    @Override
    public void onAdd() {
        dataContext.getGroupDAO().insertAll(this.getObj());
    }

    /**
     * Изменение
     */
    @Override
    public void onEdit() {
        dataContext.getGroupDAO().update(this.getObj());
    }

    /**
     * Выгрузить объект с View
     */
    @Override
    protected void loadObject() {
        getObj().setName(editText.getText().toString());
    }

    /**
     * Установить объект
     * Вычисление на изменение или на добавление объект пришел
     * @param obj
     * @return
     */
    @Override
    public ObjectFragment<Group> setObj(Group obj) {
        this.setEdit(obj.getId() != 0);
        return super.setObj(obj);
    }
}
