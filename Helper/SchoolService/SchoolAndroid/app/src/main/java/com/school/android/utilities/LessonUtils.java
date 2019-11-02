package com.school.android.utilities;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class LessonUtils {

    static HashMap<Integer, String> time;

    public static HashMap<Integer, String> getTimes() {
        if (time != null)
            return time;

        time = new HashMap<>();
        time.put(0, "8:30-9:15");
        time.put(1, "9:25-10:10");
        time.put(2, "10:30-11:15");
        time.put(3, "11:35-12:20");
        time.put(4, "12:35-13:20");
        time.put(5, "13:30-14:15");
        time.put(6, "14:30-15:15");
        time.put(7, "15:25-16:10");
        time.put(8, "16:30-17:15");
        return time;
    }

    public static List<String> getStrings() {
        ArrayList<String> strings = new ArrayList<>();
        getTimes().forEach((k, v) -> {
            strings.add(v);
        });
        return strings;
    }

    public static String getStrings(Integer number) {
        return getTimes().get(number);
    }

}
