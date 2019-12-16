package com.application.flatsandroid.app;

import android.app.Application;

import com.application.flatsandroid.R;
import com.application.flatsandroid.network.interfaces.AuthService;
import com.application.flatsandroid.network.interfaces.RealtyService;
import com.google.gson.Gson;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class App extends Application {

    private static AuthService authService;
    private static RealtyService realtyService;
    private static Integer role;

    public static Integer getRole() {
        return role;
    }

    public static void setRole(Integer role) {
        App.role = role;
    }

    public static AuthService getAuthService() {
        return authService;
    }

    public static RealtyService getRealtyService() {
        return realtyService;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        authService = createRetrofit("auth/").create(AuthService.class);
        realtyService = createRetrofit("").create(RealtyService.class);
    }

    private Retrofit createRetrofit(String baseUrl) {
        return new Retrofit.Builder().baseUrl(getString(R.string.server_url) + baseUrl)
                .addConverterFactory(GsonConverterFactory.create(new Gson())).build();
    }
}
