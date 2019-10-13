package com.example.storage.ui;

import android.view.View;
import android.widget.Button;

import com.example.storage.MainActivity;
import com.example.storage.R;

/**
 * Определение класса фрагмента для фрагмента с моделью
 * @param <T>
 */
public abstract class ModelFragment<T> extends ObjectFragment<T> {

    /**
     * Layout для возврата после выполнения действия
     */
    private final int navBack;
    /**
     * Кнопка действие
     */
    Button button;
    /**
     * Удалить
     */
    Button delete;

    public ModelFragment(int navBack) {
        this.navBack = navBack;
    }

    /**
     * Добавить обработчик для кнопок
     */
    protected void generateView() {
        button = getView().findViewById(R.id.action);
        delete = getView().findViewById(R.id.delete);
        delete.setText("Удалить");
        delete.setOnClickListener(v -> {
            onDelete();
            ((MainActivity) getActivity()).openFragment(navBack);
        });

        button.setText(isEdit() ? "Изменить" : "Добавить");

        if (isEdit())
            delete.setVisibility(View.VISIBLE);

        button.setOnClickListener(v -> {

            loadObject();
            if (ModelFragment.this.isEdit())
                onEdit();
            else
                onAdd();
            ((MainActivity) getActivity()).openFragment(navBack);
        });
    }

    /**
     * Удаление
     */
    protected abstract void onDelete();

    /**
     * Добавление
     */
    protected abstract void onAdd();

    /**
     * Изменение
     */
    protected abstract void onEdit();

    /**
     * Выгрузка объекта из View
     */
    protected abstract void loadObject();

}
