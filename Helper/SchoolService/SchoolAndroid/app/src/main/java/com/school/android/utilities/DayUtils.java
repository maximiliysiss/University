package com.school.android.utilities;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.concurrent.atomic.AtomicReference;

public class DayUtils {

    private static HashMap<Integer, String> days;

    public static HashMap<Integer, String> getDays() {
        if (days != null)
            return days;

        days = new HashMap<>();
        days.put(0, "Понедельник");
        days.put(1, "Вторник");
        days.put(2, "Среда");
        days.put(3, "Четверг");
        days.put(4, "Пятница");
        days.put(5, "Суббота");
        days.put(6, "Воскресение");
        return days;
    }

    public static String getName(Integer integer) {
        return getDays().get(integer);
    }

    public static Integer getId(String name) {
        AtomicReference<Integer> id = new AtomicReference<>(0);
        getDays().forEach((k, v) -> {
            if (name == v)
                id.set(k);
        });
        return id.get();
    }

    public static List<String> getStrings() {
        ArrayList<String> strings = new ArrayList<>();
        getDays().forEach((k, v) -> {
            strings.add(v);
        });
        return strings;
    }

}
