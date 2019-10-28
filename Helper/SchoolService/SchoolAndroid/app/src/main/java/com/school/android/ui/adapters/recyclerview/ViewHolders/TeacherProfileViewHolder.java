package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.view.View;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.LessonProfile;
import com.school.android.models.network.input.Teacher;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.threadable.Future;
import com.school.android.threadable.ThreadResult;

import java.io.IOException;

public class TeacherProfileViewHolder extends RecyclerHolder<LessonProfile> {

    private int id;

    public TeacherProfileViewHolder(@NonNull View itemView, String modelName, int id) {
        super(itemView, modelName);
        this.id = id;
    }

    @Override
    public void onClick() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity()).setTitle(R.string.delete_profile)
                .setNegativeButton(R.string.no, (dialog, which) -> {
                }).setPositiveButton(R.string.yes, (dialog, which) -> App.getLessonRetrofit().setLessonProfile(object.getId(), id).enqueue(new UniversalCallback<>(getActivity(), x -> {
                    Future<Teacher> teacherFuture = new Future<>(() -> App.getTeacherRetrofit().getModel(id).execute().body());
                    getRealActivity().openFragment(R.id.navigation_lesson_element, getString(R.string.user_model), teacherFuture.get());
                })));
        builder.create().show();
    }
}
