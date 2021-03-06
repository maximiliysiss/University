package com.application.autostation.utilities;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class DayOfWeek {

    /**
     * Дни
     */
    private static HashMap<Integer, String> days;

    /**
     * Получить дни
     *
     * @return
     */
    private static HashMap<Integer, String> getDays() {
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

    /**
     * Получить день по ID
     *
     * @param integer
     * @return
     */
    public static String getDay(Integer integer) {
        return getDays().get(integer);
    }

    /**
     * Получить только строки
     *
     * @return
     */
    public static List<String> getAllString() {
        List<String> list = new ArrayList<>();
        HashMap<Integer, String> data = getDays();
        for (int i = 0; i < data.size(); i++) {
            list.add(data.get(i));
        }
        return list;
    }

}
