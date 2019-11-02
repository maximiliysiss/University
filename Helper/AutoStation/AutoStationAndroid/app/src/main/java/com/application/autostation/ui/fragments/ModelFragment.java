package com.application.autostation.ui.fragments;

import android.app.Activity;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import androidx.fragment.app.Fragment;

import com.application.autostation.R;
import com.application.autostation.ui.models.Model;

/**
 * Фрагмент для модели
 * @param <T>
 * @param <A>
 */
public abstract class ModelFragment<T extends Model, A extends Activity> extends Fragment {

    /**
     * Модель
     */
    T obj;

    /**
     * Получить объект
     * @return
     */
    public T getObj() {
        return obj;
    }

    /**
     * Получить форму
     * @return
     */
    public A getRealActivity() {
        return (A) getActivity();
    }

    /**
     * Старт формы
     */
    @Override
    public void onStart() {
        super.onStart();

        /**
         * Получить объект
         */
        obj = (T) getArguments().getSerializable(getModelName());
    }

    /**
     * Получить название модели
     * @return
     */
    public abstract String getModelName();

    /**
     * Сгенерировать действия Добавить/Удалить/Изменить
     */
    public void generateActions() {

        View view = getView();

        Button action = view.findViewById(R.id.action);
        Button delete = view.findViewById(R.id.delete);

        if (obj.getId() == 0) {
            action.setText("Добавить");
            action.setOnClickListener(v -> {
                if (loadObject())
                    add(obj);
                else
                    Toast.makeText(getContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            });
        } else {
            delete.setVisibility(View.VISIBLE);
            delete.setOnClickListener(v -> delete(obj.getId()));
            action.setText("Изменить");
            action.setOnClickListener(v -> {
                if (loadObject())
                    update(obj);
                else
                    Toast.makeText(getContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            });
        }

    }

    /**
     * Загрузить объект с формы
     * @return
     */
    public abstract boolean loadObject();

    /**
     * Добавить
     * @param obj
     */
    public abstract void add(T obj);

    /**
     * Удалить
     * @param id
     */
    public abstract void delete(int id);

    /**
     * Изменить
     * @param obj
     */
    public abstract void update(T obj);
}
