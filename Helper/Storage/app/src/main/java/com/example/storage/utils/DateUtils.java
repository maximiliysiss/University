package com.example.storage.utils;

import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;

/**
 * Доп функции для дат
 */
public class DateUtils {
    /**
     * Месяц сегодня
     *
     * @return
     */
    public static Date today() {
        Date date = new Date();

        Calendar calendar = Calendar.getInstance();
        calendar.set(1900 + date.getYear(), date.getMonth() + 1, 0, 0, 0, 0);

        return calendar.getTime();
    }

    /**
     * Добавить месяц
     *
     * @param date
     * @param count
     * @return
     */
    public static Date addMonth(Date date, int count) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(date);
        calendar.add(Calendar.MONTH, count);
        return calendar.getTime();
    }

    /**
     * Список месяцев
     */
    private static HashMap<Integer, String> monthNames;

    /**
     * Получить месяц
     *
     * @param index
     * @return
     */
    public static String getName(int index) {
        if (monthNames != null)
            return monthNames.get(index);

        monthNames = new HashMap<>();
        monthNames.put(0, "Январь");
        monthNames.put(1, "Февраль");
        monthNames.put(2, "Март");
        monthNames.put(3, "Апрель");
        monthNames.put(4, "Май");
        monthNames.put(5, "Июнь");
        monthNames.put(6, "Июль");
        monthNames.put(7, "Август");
        monthNames.put(8, "Сентябрь");
        monthNames.put(9, "Октябрь");
        monthNames.put(10, "Ноябрь");
        monthNames.put(11, "Декабрь");
        return monthNames.get(index);
    }
}
