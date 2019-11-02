package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.models.network.input.Lesson;

public class LessonViewHolder extends RecyclerHolder<Lesson> {

    TextView name;

    public LessonViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);
        name = itemView.findViewById(R.id.name);
    }

    @Override
    public void onClick() {
        getRealActivity().openFragment(R.id.navigation_lesson_element, modelName, object);
    }

    @Override
    public void setObject(Lesson object) {
        super.setObject(object);

        name.setText(object.getName());
    }
}
