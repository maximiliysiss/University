package com.application.autostation.app;

import android.app.Application;

import androidx.room.Room;

import com.application.autostation.R;
import com.application.autostation.app.UserContext.DatabaseContext;
import com.application.autostation.app.UserContext.UserContext;
import com.application.autostation.network.interfaces.AuthRetrofit;
import com.application.autostation.network.interfaces.BuyingsRetrofit;
import com.application.autostation.network.interfaces.PointRetrofit;
import com.application.autostation.network.interfaces.ScheduleRetrofit;
import com.application.autostation.network.interfaces.UserRetrofit;
import com.application.autostation.network.models.input.LoginResult;
import com.application.autostation.utilities.NetworkUtilities;
import com.google.gson.Gson;

import java.io.IOException;
import java.net.UnknownHostException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.FutureTask;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class App extends Application {

    private static AuthRetrofit authRetrofit;
    private static ScheduleRetrofit scheduleRetrofit;
    private static BuyingsRetrofit buyingsRetrofit;
    private static PointRetrofit pointRetrofit;
    private static UserRetrofit userRetrofit;

    private static DatabaseContext databaseContext;

    public static UserRetrofit getUserRetrofit() {
        return userRetrofit;
    }

    public static PointRetrofit getPointRetrofit() {
        return pointRetrofit;
    }

    public static AuthRetrofit getAuthRetrofit() {
        return authRetrofit;
    }

    public static ScheduleRetrofit getScheduleRetrofit() {
        return scheduleRetrofit;
    }

    public static BuyingsRetrofit getBuyingsRetrofit() {
        return buyingsRetrofit;
    }

    public static void setUserContext(UserContext userContext) {
        databaseContext.userDao().delete();
        databaseContext.userDao().insert(userContext);
    }

    public static UserContext getUserContext() {
        return databaseContext.userDao().getUser();
    }

    public static UserContext tryLogin() {
        UserContext userContext = getUserContext();
        if (userContext == null)
            return null;

        FutureTask<UserContext> futureTask = new FutureTask<UserContext>(() -> {
            retrofit2.Response response = null;
            try {
                response = authRetrofit.tryConnect(userContext.getAccessToken()).execute();
                if (NetworkUtilities.isSuccess(response.code())) {
                    return new UserContext(userContext.getAccessToken(), userContext.getRefreshToken());
                } else {
                    retrofit2.Response<LoginResult> execute = authRetrofit.refresh(userContext.getAccessToken(), userContext.getRefreshToken()).execute();
                    if (NetworkUtilities.isSuccess(execute.code())) {
                        LoginResult loginResult = execute.body();
                        return new UserContext(loginResult.getAccessToken(), loginResult.getRefreshToken());
                    } else
                        return null;
                }
            } catch (IOException e) {
            }
            return null;
        });

        new Thread(futureTask).start();
        try {
            return futureTask.get();
        } catch (ExecutionException | InterruptedException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static void signOut() {
        databaseContext.userDao().delete();
    }

    @Override
    public void onCreate() {
        super.onCreate();

        databaseContext = Room.databaseBuilder(getBaseContext(), DatabaseContext.class, getString(R.string.database)).fallbackToDestructiveMigration().allowMainThreadQueries().build();

        Retrofit retrofit = new Retrofit.Builder().addConverterFactory(GsonConverterFactory.create(new Gson())).baseUrl(new StringBuilder(getString(R.string.server_url)).append("auth/").toString()).build();
        authRetrofit = retrofit.create(AuthRetrofit.class);

        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .addInterceptor(chain -> {
                    Request.Builder builder = chain.request().newBuilder();
                    String header = null;
                    UserContext userContext = getUserContext();
                    if (userContext == null)
                        return chain.proceed(builder.build());
                    retrofit2.Response response = authRetrofit.tryConnect(userContext.getAccessToken()).execute();
                    if (NetworkUtilities.isSuccess(response.code()))
                        header = userContext.getAccessToken();
                    else {
                        retrofit2.Response<LoginResult> execute = authRetrofit.refresh(userContext.getAccessToken(), userContext.getRefreshToken()).execute();
                        if (NetworkUtilities.isSuccess(execute.code())) {
                            LoginResult loginResult = execute.body();
                            header = loginResult.getAccessToken();
                            setUserContext(new UserContext(loginResult.getAccessToken(), loginResult.getRefreshToken()));
                        }
                    }

                    if (header == null)
                        throw new UnknownHostException("Authorization error");

                    builder.addHeader("Authorization", header);
                    return chain.proceed(builder.build());
                }).build();

        scheduleRetrofit = createRetrofit(okHttpClient).create(ScheduleRetrofit.class);
        buyingsRetrofit = createRetrofit(okHttpClient).create(BuyingsRetrofit.class);
        pointRetrofit = createRetrofit(okHttpClient).create(PointRetrofit.class);
        userRetrofit = createRetrofit(okHttpClient).create(UserRetrofit.class);
    }

    private Retrofit createRetrofit(OkHttpClient okHttpClient) {
        return new Retrofit.Builder().baseUrl(getString(R.string.server_url))
                .addConverterFactory(GsonConverterFactory.create(new Gson())).client(okHttpClient).build();
    }


}
