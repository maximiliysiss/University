package com.application.autostation.utilities;

public class MonthYear {
    private int year;
    private int month;

    public MonthYear(int year, int month) {
        this.year = year;
        this.month = month;
    }

    public int getYear() {
        return year;
    }

    public int getMonth() {
        return month;
    }

    public void addMonth(int count) {
        month += count;
        if (month < 1) {
            month = 12;
            year--;
        }
        if (month > 12) {
            month = 1;
            year++;
        }
    }
}
