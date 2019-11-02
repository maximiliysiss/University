package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.school.android.models.network.FragmentModel;

public class LessonProfile implements FragmentModel {
    @SerializedName("lessonId")
    @Expose
    private Integer lessonId;
    @SerializedName("id")
    @Expose
    private Integer id;
    @SerializedName("teacherId")
    @Expose
    private Integer teacherId;
    @SerializedName("teacher")
    @Expose
    private Teacher teacher;
    @SerializedName("lesson")
    @Expose
    private Lesson lesson;

    public Integer getLessonId() {
        return lessonId;
    }

    public void setLessonId(Integer lessonId) {
        this.lessonId = lessonId;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getTeacherId() {
        return teacherId;
    }

    public void setTeacherId(Integer teacherId) {
        this.teacherId = teacherId;
    }

    public Teacher getTeacher() {
        return teacher;
    }

    public void setTeacher(Teacher teacher) {
        this.teacher = teacher;
    }

    public Lesson getLesson() {
        return lesson;
    }

    public void setLesson(Lesson lesson) {
        this.lesson = lesson;
    }
}
