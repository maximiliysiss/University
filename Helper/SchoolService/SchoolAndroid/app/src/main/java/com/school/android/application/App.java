package com.school.android.application;

import android.app.Application;
import android.content.res.Resources;

import com.google.gson.Gson;
import com.school.android.R;
import com.school.android.models.network.input.Children;
import com.school.android.models.network.input.Class;
import com.school.android.models.network.input.LoginResult;
import com.school.android.models.network.input.Mark;
import com.school.android.models.network.input.Teacher;
import com.school.android.models.network.input.User;
import com.school.android.network.interfaces.AuthRetrofit;
import com.school.android.network.interfaces.ChildrenRetrofit;
import com.school.android.network.interfaces.CruRetrofit;
import com.school.android.network.interfaces.CrudRetrofit;
import com.school.android.network.interfaces.RiskGroupRetrofit;
import com.school.android.network.interfaces.ScheduleRetrofit;

import java.io.IOException;
import java.net.UnknownHostException;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class App extends Application {

    private static UserContext userContext;

    public static UserContext getUserContext() {
        return userContext;
    }

    public static void setUserContext(UserContext userContext) {
        App.userContext = userContext;
    }

    private static AuthRetrofit authRetrofit;
    private static CrudRetrofit<Class> classRetrofit;
    private static ChildrenRetrofit childrenRetrofit;
    private static CrudRetrofit<Mark> markRetrofit;
    private static RiskGroupRetrofit riskGroupRetrofit;
    private static ScheduleRetrofit scheduleRetrofit;
    private static CruRetrofit<Teacher> teacherRetrofit;
    private static CrudRetrofit<User> userRetrofit;

    private Retrofit createRetrofit(String path, OkHttpClient okHttpClient) {
        return new Retrofit.Builder().baseUrl(new StringBuilder(getString(R.string.server_url)).append(path).toString())
                .addConverterFactory(GsonConverterFactory.create(new Gson())).client(okHttpClient).build();
    }

    @Override
    public void onCreate() {
        super.onCreate();

        Retrofit retrofit = new Retrofit.Builder().addConverterFactory(GsonConverterFactory.create(new Gson())).baseUrl(new StringBuilder(getString(R.string.server_url)).append("api/auth/").toString()).build();
        authRetrofit = retrofit.create(AuthRetrofit.class);

        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .addInterceptor(new Interceptor() {
                    @Override
                    public Response intercept(Chain chain) throws IOException {
                        Request.Builder builder = new Request.Builder();
                        String header = null;
                        retrofit2.Response response = authRetrofit.tryConnect(userContext.accessToken).execute();
                        if (response.code() >= 200 && response.code() < 300)
                            header = userContext.accessToken;
                        else {
                            retrofit2.Response<LoginResult> execute = authRetrofit.refresh(userContext.accessToken, userContext.refreshToken).execute();
                            if (execute.code() >= 200 && execute.code() < 300) {
                                LoginResult loginResult = execute.body();
                                header = loginResult.getAccessToken();
                                userContext.accessToken = loginResult.getAccessToken();
                                userContext.refreshToken = loginResult.getRefreshToken();
                                userContext.type = loginResult.getUserType();
                            }
                        }

                        if (header == null)
                            throw new UnknownHostException("Authorization error");

                        builder.addHeader("Authorization", header);
                        return chain.proceed(builder.build());
                    }
                }).build();

        classRetrofit = createRetrofit("api/classes/", okHttpClient).create(CrudRetrofit.class);
        childrenRetrofit = createRetrofit("api/children", okHttpClient).create(ChildrenRetrofit.class);
        markRetrofit = createRetrofit("api/marks", okHttpClient).create(CrudRetrofit.class);
        riskGroupRetrofit = createRetrofit("api/riskgroups", okHttpClient).create(RiskGroupRetrofit.class);
        scheduleRetrofit = createRetrofit("api/schedules", okHttpClient).create(ScheduleRetrofit.class);
        teacherRetrofit = createRetrofit("api/teachers", okHttpClient).create(CruRetrofit.class);
        userRetrofit = createRetrofit("api/users", okHttpClient).create(CrudRetrofit.class);
    }

    public static AuthRetrofit getAuthRetrofit() {
        return authRetrofit;
    }

    public static CrudRetrofit<Class> getClassRetrofit() {
        return classRetrofit;
    }

    public static ChildrenRetrofit getChildrenRetrofit() {
        return childrenRetrofit;
    }

    public static CrudRetrofit<Mark> getMarkRetrofit() {
        return markRetrofit;
    }

    public static RiskGroupRetrofit getRiskGroupRetrofit() {
        return riskGroupRetrofit;
    }

    public static ScheduleRetrofit getScheduleRetrofit() {
        return scheduleRetrofit;
    }

    public static CruRetrofit<Teacher> getTeacherRetrofit() {
        return teacherRetrofit;
    }

    public static CrudRetrofit<User> getUserRetrofit() {
        return userRetrofit;
    }
}
