package com.school.android.utilities;

import java.util.Calendar;
import java.util.Date;

public class CustomDate {
    private int year;
    private int month;
    private int day;

    public CustomDate() {
    }

    public CustomDate(String s) {
        parse(s);
    }

    public CustomDate(int year, int month, int day) {
        this.year = year;
        this.month = month;
        this.day = day;
    }

    public void parse(String str) {
        String[] parts = str.split(".");
        year = Integer.parseInt(parts[0]);
        month = Integer.parseInt(parts[1]);
        day = Integer.parseInt(parts[2]);
    }

    public Calendar toCalendar() {
        Calendar calendar = Calendar.getInstance();
        calendar.set(year, month - 1, day, 0, 0);
        return calendar;
    }

    public CustomDate(long time) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(time);
        year = calendar.get(Calendar.YEAR);
        month = calendar.get(Calendar.MONTH) + 1;
        day = calendar.get(Calendar.DAY_OF_MONTH);
    }

    @Override
    public String toString() {
        return "" + year + "." + month + "." + day;
    }

    public int getDayOfWeek() {
        return toCalendar().get(Calendar.DAY_OF_WEEK) - 2;
    }

    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        this.year = year;
    }

    public int getMonth() {
        return month;
    }

    public void setMonth(int month) {
        this.month = month;
    }

    public int getDay() {
        return day;
    }

    public void setDay(int day) {
        this.day = day;
    }
}
