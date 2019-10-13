package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Mark {
    @SerializedName("id")
    @Expose
    private Integer id;
    @SerializedName("markReal")
    @Expose
    private Integer markReal;
    @SerializedName("teacherId")
    @Expose
    private Integer teacherId;
    @SerializedName("childId")
    @Expose
    private Integer childId;
    @SerializedName("scheduleId")
    @Expose
    private Integer scheduleId;
    @SerializedName("schedule")
    @Expose
    private Schedule schedule;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getMarkReal() {
        return markReal;
    }

    public void setMarkReal(Integer markReal) {
        this.markReal = markReal;
    }

    public Integer getTeacherId() {
        return teacherId;
    }

    public void setTeacherId(Integer teacherId) {
        this.teacherId = teacherId;
    }

    public Integer getChildId() {
        return childId;
    }

    public void setChildId(Integer childId) {
        this.childId = childId;
    }

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
}
