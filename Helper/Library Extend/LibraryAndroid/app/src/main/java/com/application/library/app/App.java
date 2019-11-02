package com.application.library.app;

import android.app.Application;

import androidx.room.Room;

import com.application.library.R;
import com.application.library.app.UserContext.DbContext;
import com.application.library.app.UserContext.UserContext;
import com.application.library.network.interfaces.AuthRetrofit;
import com.application.library.network.interfaces.BookRetrofit;
import com.application.library.network.models.input.LoginResult;
import com.application.library.utilities.NetworkUtilities;
import com.google.gson.Gson;

import java.io.IOException;
import java.net.UnknownHostException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.FutureTask;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Переопределим главный класс для приложения
 */
public class App extends Application {

    /**
     * Интерфейсы для подключения к серверу
     */
    private static AuthRetrofit authRetrofit;
    private static BookRetrofit bookRetrofit;

    /**
     * Доступ к БД для хранения пользователя
     */
    private static DbContext dbContext;

    public static AuthRetrofit getAuthRetrofit() {
        return authRetrofit;
    }

    public static BookRetrofit getBookRetrofit() {
        return bookRetrofit;
    }

    /**
     * Установим пользователя. Удалим -> Добавим пользователя
     * @param userContext
     */
    public static void setUserContext(UserContext userContext) {
        dbContext.userDao().delete();
        dbContext.userDao().insert(userContext);
    }

    /**
     * Получить текущего пользователя
     * @return
     */
    public static UserContext getUserContext() {
        return dbContext.userDao().getUser();
    }

    /**
     * Попробовать войти по пользователю из БД
     * @return Пользователь
     */
    public static UserContext tryLogin() {
        UserContext userContext = getUserContext();
        if (userContext == null)
            return null;

        FutureTask<UserContext> futureTask = new FutureTask<UserContext>(() -> {
            retrofit2.Response response = null;
            try {
                response = authRetrofit.tryConnect(userContext.getAccessToken()).execute();
                if (NetworkUtilities.isSuccess(response.code())) {
                    return new UserContext(userContext.getAccessToken(), userContext.getRefreshToken(), userContext.getUserRole());
                } else {
                    retrofit2.Response<LoginResult> execute = authRetrofit.refresh(userContext.getAccessToken(), userContext.getRefreshToken()).execute();
                    if (NetworkUtilities.isSuccess(execute.code())) {
                        LoginResult loginResult = execute.body();
                        return new UserContext(loginResult.getAccessToken(), loginResult.getRefreshToken(), loginResult.getUserRole());
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

    /**
     * Выйти = удалить
     */
    public static void signOut() {
        dbContext.userDao().delete();
    }

    /**
     * Создание приложения
     */
    @Override
    public void onCreate() {
        super.onCreate();

        /**
         * Создадим подключение к БД
         */
        dbContext = Room.databaseBuilder(getBaseContext(), DbContext.class, getString(R.string.database)).allowMainThreadQueries().build();

        /**
         * Добавим подклчюение к AuthController у сервера
         */
        Retrofit retrofit = new Retrofit.Builder().addConverterFactory(GsonConverterFactory.create(new Gson())).baseUrl(new StringBuilder(getString(R.string.server_url)).append("auth/").toString()).build();
        authRetrofit = retrofit.create(AuthRetrofit.class);

        /**
         * Добавим интерсептор. Он позволяет изменять запросы и дописывать в Header информацию об авторизации
         */
        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .addInterceptor(chain -> {
                    Request.Builder builder = chain.request().newBuilder();
                    String header = null;
                    UserContext userContext = getUserContext();
                    /**
                     * Попытка доступа
                     */
                    retrofit2.Response response = authRetrofit.tryConnect(userContext.getAccessToken()).execute();
                    if (NetworkUtilities.isSuccess(response.code()))
                        header = userContext.getAccessToken();
                    else {
                        /**
                         * Попытка обновить токены
                         */
                        retrofit2.Response<LoginResult> execute = authRetrofit.refresh(userContext.getAccessToken(), userContext.getRefreshToken()).execute();
                        if (NetworkUtilities.isSuccess(execute.code())) {
                            LoginResult loginResult = execute.body();
                            header = loginResult.getAccessToken();
                            setUserContext(new UserContext(loginResult.getAccessToken(), loginResult.getRefreshToken(), loginResult.getUserRole()));
                        }
                    }

                    if (header == null)
                        throw new UnknownHostException("Authorization error");

                    builder.addHeader("Authorization", header);
                    return chain.proceed(builder.build());
                }).build();

        /**
         * Добавим подключение к BookController
         */
        bookRetrofit = createRetrofit(okHttpClient).create(BookRetrofit.class);
    }

    /**
     * Создаение подключения к серверу
     * @param okHttpClient
     * @return
     */
    private Retrofit createRetrofit(OkHttpClient okHttpClient) {
        return new Retrofit.Builder().baseUrl(getString(R.string.server_url))
                .addConverterFactory(GsonConverterFactory.create(new Gson())).client(okHttpClient).build();
    }

}
