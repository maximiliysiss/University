package com.example.testangryandroid.app;

import android.app.Application;
import android.content.Intent;

import com.example.testangryandroid.R;
import com.example.testangryandroid.app.UserContext.UserContext;
import com.example.testangryandroid.network.NetworkUtilities;
import com.example.testangryandroid.network.interfaces.AuthRetrofit;
import com.example.testangryandroid.network.interfaces.ExecutedRetrofit;
import com.example.testangryandroid.network.interfaces.QuestionRetrofit;
import com.example.testangryandroid.network.models.LoginResult;
import com.google.gson.Gson;

import java.io.IOException;
import java.net.UnknownHostException;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

import static com.example.testangryandroid.app.UserContext.DatabaseContext.getUserContext;
import static com.example.testangryandroid.app.UserContext.DatabaseContext.setUserContext;

/**
 * Application class
 */
public class App extends Application {

    /**
     * Connections to server
     */
    private static QuestionRetrofit questionRetrofit;
    private static ExecutedRetrofit executedRetrofit;
    private static AuthRetrofit authRetrofit;

    /**
     * Current user
     */
    private static String userName;

    public static String getUserName() {
        return userName;
    }

    public static void setUserName(String userName) {
        App.userName = userName;
    }

    public static QuestionRetrofit getQuestionRetrofit() {
        return questionRetrofit;
    }

    public static ExecutedRetrofit getExecutedRetrofit() {
        return executedRetrofit;
    }

    public static AuthRetrofit getAuthRetrofit() {
        return authRetrofit;
    }

    @Override
    public void onCreate() {
        super.onCreate();

        /**
         * Create connections
         */
        Retrofit authRetrofit = new Retrofit.Builder().baseUrl(getString(R.string.server_url)).addConverterFactory(GsonConverterFactory.create(new Gson())).build();
        this.authRetrofit = authRetrofit.create(AuthRetrofit.class);


        OkHttpClient okHttpClient = new OkHttpClient.Builder().addInterceptor(new Interceptor() {
            @Override
            public Response intercept(Chain chain) throws IOException {
                Request.Builder builder = chain.request().newBuilder();
                String header = null;
                UserContext userContext = getUserContext();
                /**
                 * Если пользователя нету
                 */
                if (userContext == null)
                    return chain.proceed(builder.build());
                /**
                 * Попытка подключиться
                 */
                retrofit2.Response response = App.this.authRetrofit.tryLogin(userContext.getAccessToken()).execute();
                if (NetworkUtilities.isSuccess(response.code()))
                    header = userContext.getAccessToken();
                else {
                    /**
                     * Обновим токены
                     */
                    retrofit2.Response<LoginResult> execute = App.this.authRetrofit.refreshToken(userContext.getAccessToken(), userContext.getRefreshToken()).execute();
                    if (NetworkUtilities.isSuccess(execute.code())) {
                        LoginResult loginResult = execute.body();
                        header = "Bearer " + loginResult.getAccessToken();
                        setUserContext(new UserContext(loginResult.getAccessToken(), loginResult.getRefreshToken()));
                    }
                }

                if (header == null)
                    throw new UnknownHostException("Authorization error");

                builder.addHeader("Authorization", header);
                return chain.proceed(builder.build());
            }
        }).build();
        Retrofit retrofit = new Retrofit.Builder().client(okHttpClient).baseUrl(getString(R.string.server_url)).addConverterFactory(GsonConverterFactory.create(new Gson())).build();
        questionRetrofit = retrofit.create(QuestionRetrofit.class);
        executedRetrofit = retrofit.create(ExecutedRetrofit.class);
    }
}
