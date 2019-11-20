package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.app.AlertDialog;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.models.network.input.LessonProfile;
import com.school.android.models.network.input.Teacher;
import com.school.android.network.classes.UniversalCallback;
import com.school.android.threadable.Future;

public class TeacherProfileViewHolder extends RecyclerHolder<LessonProfile> {

    private final int backLayout;
    private boolean withoutStudent;
    private int id;
    TextView name;

    public TeacherProfileViewHolder(@NonNull View itemView, String modelName, int id, boolean withoutStudent, int backLayout) {
        super(itemView, modelName);
        this.id = id;
        this.withoutStudent = withoutStudent;
        this.backLayout = backLayout;
        name = itemView.findViewById(R.id.name);
    }

    @Override
    public void onClick() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity()).setTitle(R.string.delete_profile)
                .setNegativeButton(R.string.no, (dialog, which) -> {
                }).setPositiveButton(R.string.yes, (dialog, which) -> {
                    App.getLessonRetrofit().setLessonProfile(object.getLessonId(), id).enqueue(new UniversalCallback<>(getActivity(), x -> {
                        Future<Teacher> teacherFuture = new Future<>(() -> App.getTeacherRetrofit().getModel(id).execute().body());
                        Bundle bundle = new Bundle();
                        bundle.putBoolean(getString(R.string.without_student), withoutStudent);
                        bundle.putInt(getString(R.string.back), backLayout);
                        getRealActivity().openFragment(R.id.navigation_users_element, getString(R.string.user_model), teacherFuture.get(), bundle);
                    }));
                });
        builder.create().show();
    }


    @Override
    public void setObject(LessonProfile object) {
        super.setObject(object);

        name.setText(object.getLesson().getName());
    }
}
