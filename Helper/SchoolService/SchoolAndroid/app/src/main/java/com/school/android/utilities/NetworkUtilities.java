package com.school.android.utilities;

public class NetworkUtilities {

    public static boolean isSuccess(int code) {
        return code >= 200 && code < 300;
    }

}
