package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.school.android.models.network.FragmentModel;

public class Schedule implements FragmentModel {
    @SerializedName("id")
    @Expose
    private Integer id = 0;
    @SerializedName("classId")
    @Expose
    private Integer classId;
    @SerializedName("class")
    @Expose
    private Class _class;
    @SerializedName("lessonNumber")
    @Expose
    private Integer lessonNumber;
    @SerializedName("lessonId")
    @Expose
    private Integer lessonId;
    @SerializedName("lesson")
    @Expose
    private Lesson lesson;
    @SerializedName("teacherId")
    @Expose
    private Integer teacherId;
    @SerializedName("dayOfWeek")
    @Expose
    private Integer dayOfWeek;
    @SerializedName("isFacultative")
    @Expose
    private Boolean isFacultative = false;

    public Integer getLessonId() {
        return lessonId;
    }

    public void setLessonId(Integer lessonId) {
        this.lessonId = lessonId;
    }

    public void setLesson(Lesson lesson) {
        this.lesson = lesson;
    }

    public Lesson getLesson() {
        return lesson;
    }

    public Class get_class() {
        return _class;
    }

    public void set_class(Class _class) {
        this._class = _class;
    }

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
