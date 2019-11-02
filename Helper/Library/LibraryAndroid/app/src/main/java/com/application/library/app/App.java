package com.application.library.app;

import android.app.Application;

import androidx.room.Room;

import com.application.library.app.UserContext.DatabaseContext;
import com.application.library.app.UserContext.UserContext;
import com.application.library.network.interfaces.AuthRetrofit;
import com.application.library.network.interfaces.BookRetrofit;
import com.application.library.network.models.input.LoginResult;
import com.application.library.utilities.NetworkUtilities;
import com.google.gson.Gson;
import com.school.library.R;

import java.io.IOException;
import java.net.UnknownHostException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.FutureTask;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Класс приложения
 */
public class App extends Application {

    /**
     * Доступ к серверу
     */
    private static AuthRetrofit authRetrofit;
    private static BookRetrofit bookRetrofit;

    /**
     * Подключение к БД
     */
    private static DatabaseContext databaseContext;

    public static AuthRetrofit getAuthRetrofit() {
        return authRetrofit;
    }

    public static BookRetrofit getBookRetrofit() {
        return bookRetrofit;
    }

    /**
     * Установить пользователя через удаление всех остальных
     *
     * @param userContext
     */
    public static void setUserContext(UserContext userContext) {
        databaseContext.userDao().delete();
        databaseContext.userDao().insert(userContext);
    }

    /**
     * Получить пользователя из БД
     *
     * @return
     */
    public static UserContext getUserContext() {
        return databaseContext.userDao().getUser();
    }

    /**
     * Попытка авторизоваться при старте приложения
     *
     * @return
     */
    public static UserContext tryLogin() {
        UserContext userContext = getUserContext();
        /**
         * Если пользователь не установлен, то ничего не нашли
         */
        if (userContext == null)
            return null;

        /**
         * Попробуем получить информацию
         */
        FutureTask<UserContext> futureTask = new FutureTask<UserContext>(() -> {
            retrofit2.Response response = null;
            try {
                /**
                 * Попробуем проверить текущие данные
                 */
                response = authRetrofit.tryConnect(userContext.getAccessToken()).execute();
                if (NetworkUtilities.isSuccess(response.code())) {
                    return new UserContext(userContext.getAccessToken(), userContext.getRefreshToken(), userContext.getUserRole());
                } else {
                    /**
                     * Обновим токен
                     */
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
        databaseContext.userDao().delete();
    }

    /**
     * Создание приложения
     */
    @Override
    public void onCreate() {
        super.onCreate();

        /**
         * Опеределние БД
         */
        databaseContext = Room.databaseBuilder(getBaseContext(), DatabaseContext.class, getString(R.string.database)).allowMainThreadQueries().build();

        /**
         * Подключение к авторизации на сервере
         */
        Retrofit retrofit = new Retrofit.Builder().addConverterFactory(GsonConverterFactory.create(new Gson())).baseUrl(new StringBuilder(getString(R.string.server_url)).append("auth/").toString()).build();
        authRetrofit = retrofit.create(AuthRetrofit.class);

        /**
         * Определим интерсептор, чтобы дополнять все запросы информацией о авторизации
         */
        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .addInterceptor(chain -> {
                    Request.Builder builder = chain.request().newBuilder();
                    String header = null;
                    UserContext userContext = getUserContext();
                    /**
                     * Попробуем авторизироваться
                     */
                    retrofit2.Response response = authRetrofit.tryConnect(userContext.getAccessToken()).execute();
                    if (NetworkUtilities.isSuccess(response.code()))
                        header = userContext.getAccessToken();
                    else {
                        /**
                         * Обновим токен
                         */
                        retrofit2.Response<LoginResult> execute = authRetrofit.refresh(userContext.getAccessToken(), userContext.getRefreshToken()).execute();
                        if (NetworkUtilities.isSuccess(execute.code())) {
                            LoginResult loginResult = execute.body();
                            header = loginResult.getAccessToken();
                            setUserContext(new UserContext(loginResult.getAccessToken(), loginResult.getRefreshToken(), loginResult.getUserRole()));
                        }
                    }

                    /**
                     * Если никак не может получить доступ
                     */
                    if (header == null)
                        throw new UnknownHostException("Authorization error");

                    builder.addHeader("Authorization", header);
                    return chain.proceed(builder.build());
                }).build();

        bookRetrofit = createRetrofit(okHttpClient).create(BookRetrofit.class);
    }

    /**
     * Создание доступа к серверу
     *
     * @param okHttpClient
     * @return
     */
    private Retrofit createRetrofit(OkHttpClient okHttpClient) {
        return new Retrofit.Builder().baseUrl(getString(R.string.server_url))
                .addConverterFactory(GsonConverterFactory.create(new Gson())).client(okHttpClient).build();
    }

}
