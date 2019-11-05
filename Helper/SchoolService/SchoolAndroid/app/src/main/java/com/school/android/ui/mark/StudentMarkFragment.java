package com.school.android.ui.mark;

import com.school.android.application.App;
import com.school.android.models.network.input.Children;
import com.school.android.threadable.Future;

public class StudentMarkFragment extends SuperTeacherFragment {

    @Override
    public int getClassId() {
        Future<Children> future = new Future<>(() -> App.getChildrenRetrofit().getModel(App.getUserContext().id).execute().body());
        return future.get().getClass_().getId();
    }
}
