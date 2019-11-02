package com.application.library.utilities;

public class NetworkUtilities {

    /**
     * Проверка кода результата на успешность
     *
     * @param code
     * @return
     */
    public static boolean isSuccess(int code) {
        return code >= 200 && code < 400;
    }

}
