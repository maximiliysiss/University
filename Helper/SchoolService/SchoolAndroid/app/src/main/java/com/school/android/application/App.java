package com.school.android.application;

import android.app.Application;

import com.google.gson.Gson;
import com.school.android.R;
import com.school.android.models.network.input.LoginResult;
import com.school.android.network.interfaces.AuthRetrofit;
import com.school.android.network.interfaces.ChildrenRetrofit;
import com.school.android.network.interfaces.ClassRetrofit;
import com.school.android.network.interfaces.MarkRetrofit;
import com.school.android.network.interfaces.RiskGroupRetrofit;
import com.school.android.network.interfaces.ScheduleRetrofit;
import com.school.android.network.interfaces.TeacherRetrofit;
import com.school.android.network.interfaces.UserRetrofit;
import com.school.android.utilities.NetworkUtilities;

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
    private static ClassRetrofit classRetrofit;
    private static ChildrenRetrofit childrenRetrofit;
    private static MarkRetrofit markRetrofit;
    private static RiskGroupRetrofit riskGroupRetrofit;
    private static ScheduleRetrofit scheduleRetrofit;
    private static TeacherRetrofit teacherRetrofit;
    private static UserRetrofit userRetrofit;

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
                        Request.Builder builder = chain.request().newBuilder();
                        String header = null;
                        retrofit2.Response response = authRetrofit.tryConnect(userContext.accessToken).execute();
                        if (NetworkUtilities.isSuccess(response.code()))
                            header = userContext.accessToken;
                        else {
                            retrofit2.Response<LoginResult> execute = authRetrofit.refresh(userContext.accessToken, userContext.refreshToken).execute();
                            if (NetworkUtilities.isSuccess(execute.code())) {
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

        classRetrofit = createRetrofit("api/classes/", okHttpClient).create(ClassRetrofit.class);
        childrenRetrofit = createRetrofit("api/children/", okHttpClient).create(ChildrenRetrofit.class);
        markRetrofit = createRetrofit("api/marks/", okHttpClient).create(MarkRetrofit.class);
        riskGroupRetrofit = createRetrofit("api/riskgroups/", okHttpClient).create(RiskGroupRetrofit.class);
        scheduleRetrofit = createRetrofit("api/schedules/", okHttpClient).create(ScheduleRetrofit.class);
        teacherRetrofit = createRetrofit("api/teachers/", okHttpClient).create(TeacherRetrofit.class);
        userRetrofit = createRetrofit("api/users/", okHttpClient).create(UserRetrofit.class);
    }

    public static AuthRetrofit getAuthRetrofit() {
        return authRetrofit;
    }

    public static ClassRetrofit getClassRetrofit() {
        return classRetrofit;
    }

    public static ChildrenRetrofit getChildrenRetrofit() {
        return childrenRetrofit;
    }

    public static MarkRetrofit getMarkRetrofit() {
        return markRetrofit;
    }

    public static RiskGroupRetrofit getRiskGroupRetrofit() {
        return riskGroupRetrofit;
    }

    public static ScheduleRetrofit getScheduleRetrofit() {
        return scheduleRetrofit;
    }

    public static TeacherRetrofit getTeacherRetrofit() {
        return teacherRetrofit;
    }

    public static UserRetrofit getUserRetrofit() {
        return userRetrofit;
    }
}
