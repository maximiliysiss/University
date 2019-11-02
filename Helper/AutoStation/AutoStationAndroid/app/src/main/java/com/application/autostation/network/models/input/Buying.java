package com.application.autostation.network.models.input;

import com.application.autostation.ui.models.Model;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Покупки
 */
public class Buying {
    @SerializedName("scheduleId")
    @Expose
    private Integer scheduleId;
    @SerializedName("schedule")
    @Expose
    private Schedule schedule;
    @SerializedName("historySchedule")
    @Expose
    private String historySchedule;
    @SerializedName("count")
    @Expose
    private Integer count;
    @SerializedName("sum")
    @Expose
    private Integer sum;
    @SerializedName("guidNumber")
    @Expose
    private String guidNumber;
    @SerializedName("dateTime")
    @Expose
    private String dateTime;

    public Integer getScheduleId() {
        return scheduleId;
    }

    public void setScheduleId(Integer scheduleId) {
        this.scheduleId = scheduleId;
    }

    public Schedule getSchedule() {
        return schedule;
    }

    public void setSchedule(Schedule schedule) {
        this.schedule = schedule;
    }

    public String getHistorySchedule() {
        return historySchedule;
    }

    public void setHistorySchedule(String historySchedule) {
        this.historySchedule = historySchedule;
    }

    public Integer getCount() {
        return count;
    }

    public void setCount(Integer count) {
        this.count = count;
    }

    public Integer getSum() {
        return sum;
    }

    public void setSum(Integer sum) {
        this.sum = sum;
    }

    public String getGuidNumber() {
        return guidNumber;
    }

    public void setGuidNumber(String guidNumber) {
        this.guidNumber = guidNumber;
    }

    public String getDateTime() {
        return dateTime;
    }

    public void setDateTime(String dateTime) {
        this.dateTime = dateTime;
    }
}
