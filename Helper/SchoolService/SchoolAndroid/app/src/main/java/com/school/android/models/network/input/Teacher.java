package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.school.android.models.extension.UserType;

import java.util.List;

public class Teacher extends User {

    public Teacher() {
        userType = UserType.Teacher.ordinal();
    }

    @SerializedName("isClassWork")
    @Expose
    private Boolean isClassWork;
    @SerializedName("class")
    @Expose
    private List<Class> _class = null;
    @SerializedName("marks")
    @Expose
    private List<Mark> marks = null;
    @SerializedName("lessonProfiles")
    @Expose
    private List<LessonProfile> lessonProfiles = null;

    public List<LessonProfile> getLessonProfiles() {
        return lessonProfiles;
    }

    public void setLessonProfiles(List<LessonProfile> lessonProfiles) {
        this.lessonProfiles = lessonProfiles;
    }

    public Boolean getIsClassWork() {
        return isClassWork;
    }

    public void setIsClassWork(Boolean isClassWork) {
        this.isClassWork = isClassWork;
    }

    public List<Class> getClass_() {
        return _class;
    }

    public void setClass_(List<Class> _class) {
        this._class = _class;
    }

    public List<Mark> getMarks() {
        return marks;
    }

    public void setMarks(List<Mark> marks) {
        this.marks = marks;
    }
}
