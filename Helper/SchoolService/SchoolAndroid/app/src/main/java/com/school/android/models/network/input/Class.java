package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.school.android.models.network.FragmentModel;

import java.util.List;

public class Class implements FragmentModel {
    @SerializedName("id")
    @Expose
    private Integer id = 0;
    @SerializedName("name")
    @Expose
    private String name = null;
    @SerializedName("teacherId")
    @Expose
    private Integer teacherId;
    @SerializedName("teacher")
    @Expose
    private Teacher teacher;
    @SerializedName("isStart")
    @Expose
    private Boolean isStartClass = false;
    @SerializedName("children")
    @Expose
    private List<Children> children = null;

    public Boolean getStartClass() {
        return isStartClass;
    }

    public void setStartClass(Boolean startClass) {
        isStartClass = startClass;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
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

    public List<Children> getChildren() {
        return children;
    }

    public void setChildren(List<Children> children) {
        this.children = children;
    }
}
