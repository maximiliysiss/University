package com.application.autostation.utilities;

public class NetworkUtilities {

    /**
     * Успешен ли код
     *
     * @param code
     * @return
     */
    public static boolean isSuccess(int code) {
        return code >= 200 && code < 400;
    }

}
