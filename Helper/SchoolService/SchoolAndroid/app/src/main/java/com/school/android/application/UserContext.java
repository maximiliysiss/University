package com.school.android.application;

import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Teacher;
import com.school.android.models.network.input.User;
import com.school.android.threadable.Future;
import com.school.android.threadable.ThreadResult;

import java.io.IOException;

import retrofit2.Response;

public class UserContext {
    public String refreshToken;
    public String accessToken;
    public Integer type;
    public Integer id;

    public UserContext(String refreshToken, String accessToken, Integer type, Integer id) {
        this.refreshToken = refreshToken;
        this.accessToken = accessToken;
        this.type = type;
        this.id = id;
    }

    public User getUser() {
        Future<User> userGet = new Future<>(new ThreadResult<User>() {
            @Override
            public User get() throws IOException {
                Response<User> response = App.getUserRetrofit().getModel(id).execute();
                if (response.code() >= 200 && response.code() < 300) {
                    if (response.body().getUserType() != UserType.Teacher.ordinal())
                        return response.body();

                    return new Future<>(new ThreadResult<Teacher>() {
                        @Override
                        public Teacher get() throws IOException {
                            return App.getTeacherRetrofit().getModel(id).execute().body();
                        }
                    }).get();
                }
                return null;
            }
        });
        return userGet.get();

    }
}
