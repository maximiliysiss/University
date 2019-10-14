package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Schedule {
    @SerializedName("id")
    @Expose
    private Integer id;
    @SerializedName("classId")
    @Expose
    private Integer classId;
    @SerializedName("lessonNumber")
    @Expose
    private Integer lessonNumber;
    @SerializedName("lesson")
    @Expose
    private String lesson;
    @SerializedName("teacherId")
    @Expose
    private Integer teacherId;
    @SerializedName("dayOfWeek")
    @Expose
    private Integer dayOfWeek;
    @SerializedName("isFacultative")
    @Expose
    private Boolean isFacultative;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getClassId() {
        return classId;
    }

    public void setClassId(Integer classId) {
        this.classId = classId;
    }

    public Integer getLessonNumber() {
        return lessonNumber;
    }

    public void setLessonNumber(Integer lessonNumber) {
        this.lessonNumber = lessonNumber;
    }

    public String getLesson() {
        return lesson;
    }

    public void setLesson(String lesson) {
        this.lesson = lesson;
    }

    public Integer getTeacherId() {
        return teacherId;
    }

    public void setTeacherId(Integer teacherId) {
        this.teacherId = teacherId;
    }

    public Integer getDayOfWeek() {
        return dayOfWeek;
    }

    public void setDayOfWeek(Integer dayOfWeek) {
        this.dayOfWeek = dayOfWeek;
    }

    public Boolean getIsFacultative() {
        return isFacultative;
    }

    public void setIsFacultative(Boolean isFacultative) {
        this.isFacultative = isFacultative;
    }

}
