package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.school.android.models.network.FragmentModel;

public class Mark implements FragmentModel {
    @SerializedName("id")
    @Expose
    private Integer id = 0;
    @SerializedName("markReal")
    @Expose
    private Integer markReal = 0;
    @SerializedName("teacherId")
    @Expose
    private Integer teacherId;
    @SerializedName("childId")
    @Expose
    private Integer childId;
    @SerializedName("child")
    @Expose
    private Children child;
    @SerializedName("scheduleId")
    @Expose
    private Integer scheduleId;
    @SerializedName("schedule")
    @Expose
    private Schedule schedule;
    @SerializedName("dateJson")
    @Expose
    private String dateJson;

    public String getDateJson() {
        return dateJson;
    }

    public void setDateJson(String dateJson) {
        this.dateJson = dateJson;
    }

    public Children getChild() {
        return child;
    }

    public void setChild(Children child) {
        this.child = child;
    }

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
