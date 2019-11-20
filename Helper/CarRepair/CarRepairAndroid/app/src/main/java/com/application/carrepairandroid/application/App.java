package com.application.carrepairandroid.application;

import android.app.Application;

import androidx.room.Room;

import com.application.carrepairandroid.R;
import com.application.carrepairandroid.application.UserContext.DatabaseContext;
import com.application.carrepairandroid.application.UserContext.UserContext;
import com.application.carrepairandroid.network.interfaces.AuthRetrofit;
import com.application.carrepairandroid.network.interfaces.ServiceRetrofit;
import com.application.carrepairandroid.network.models.input.LoginResult;
import com.application.carrepairandroid.utilities.NetworkUtilities;
import com.google.gson.Gson;

import java.net.UnknownHostException;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Класс приложения
 */
public class App extends Application {

    /**
     * Подключение к БД
     */
    private static DatabaseContext databaseContext;

    public static UserContext getUserContext() {
        return databaseContext.userDao().getUser();
    }

    public static void setUserContext(UserContext userContext) {
        databaseContext.userDao().delete();
        databaseContext.userDao().insert(userContext);
    }

    /**
     * Подключение к серверу
     */
    private static AuthRetrofit authRetrofit;

    private static ServiceRetrofit serviceRetrofit;

    public static ServiceRetrofit getServiceRetrofit() {
        return serviceRetrofit;
    }

    public static void signOut() {
        databaseContext.userDao().delete();
    }

    private Retrofit createRetrofit(OkHttpClient okHttpClient) {
        return new Retrofit.Builder().baseUrl(getString(R.string.server_url))
                .addConverterFactory(GsonConverterFactory.create(new Gson())).client(okHttpClient).build();
    }

    @Override
    public void onCreate() {
        super.onCreate();

        databaseContext = Room.databaseBuilder(getBaseContext(), DatabaseContext.class, getString(R.string.db_car)).allowMainThreadQueries()
                .fallbackToDestructiveMigration().build();

        Retrofit retrofit = new Retrofit.Builder().addConverterFactory(GsonConverterFactory.create(new Gson())).baseUrl(new StringBuilder(getString(R.string.server_url)).append("auth/").toString()).build();
        authRetrofit = retrofit.create(AuthRetrofit.class);

        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .addInterceptor(chain -> {
                    Request.Builder builder = chain.request().newBuilder();
                    String header = null;
                    UserContext userContext = getUserContext();
                    if (userContext == null)
                        return chain.proceed(builder.build());
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
                            setUserContext(userContext);
                        }
                    }

                    if (header == null)
                        throw new UnknownHostException("Authorization error");

                    builder.addHeader("Authorization", header);
                    return chain.proceed(builder.build());
                }).build();

        serviceRetrofit = createRetrofit(okHttpClient).create(ServiceRetrofit.class);
    }

    public static AuthRetrofit getAuthRetrofit() {
        return authRetrofit;
    }
}
