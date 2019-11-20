package com.application.carrepairandroid.utilities;

/**
 * Утилиты для сети
 */
public class NetworkUtilities {
    /**
     * Успешность кода
     * @param code
     * @return
     */
    public static boolean isSuccess(int code) {
        return code >= 200 && code < 300;
    }
}
