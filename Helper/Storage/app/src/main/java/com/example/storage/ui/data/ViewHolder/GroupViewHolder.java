package com.example.storage.ui.data.ViewHolder;

import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.example.storage.MainActivity;
import com.example.storage.R;
import com.example.storage.data.models.Group;
import com.example.storage.ui.group.GroupElementFragment;

/**
 * Определение для элемента группы в RecyclerView
 */
public class GroupViewHolder extends CardViewHolder<Group> {

    /**
     * Поле вывода
     */
    TextView textView;

    public GroupViewHolder(@NonNull View itemView) {
        super(itemView);
        textView = itemView.findViewById(R.id.name);
    }

    /**
     * Обработка click
     * Просто открыть другой фрагмент для редактирования элемента
     */
    @Override
    public void click() {
        ((MainActivity)getActivity()).openFragment(R.id.navigation_group_element, this.obj, "Group");
    }

    /**
     * Установить объект
     * Выставить данные в View
     * @param obj
     */
    @Override
    public void setObj(Group obj) {
        super.setObj(obj);
        textView.setText(obj.getName());
    }
}
