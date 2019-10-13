package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

class Teacher {
    @SerializedName("isClassWork")
    @Expose
    private Boolean isClassWork;
    @SerializedName("class")
    @Expose
    private List<Class> _class = null;
    @SerializedName("marks")
    @Expose
    private List<Mark> marks = null;
    @SerializedName("id")
    @Expose
    private Integer id;
    @SerializedName("login")
    @Expose
    private String login;
    @SerializedName("passwordHash")
    @Expose
    private String passwordHash;
    @SerializedName("name")
    @Expose
    private String name;
    @SerializedName("surname")
    @Expose
    private String surname;
    @SerializedName("secondName")
    @Expose
    private String secondName;
    @SerializedName("birthday")
    @Expose
    private String birthday;
    @SerializedName("userType")
    @Expose
    private Integer userType;

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

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getLogin() {
        return login;
    }

    public void setLogin(String login) {
        this.login = login;
    }

    public String getPasswordHash() {
        return passwordHash;
    }

    public void setPasswordHash(String passwordHash) {
        this.passwordHash = passwordHash;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public String getSecondName() {
        return secondName;
    }

    public void setSecondName(String secondName) {
        this.secondName = secondName;
    }

    public String getBirthday() {
        return birthday;
    }

    public void setBirthday(String birthday) {
        this.birthday = birthday;
    }

    public Integer getUserType() {
        return userType;
    }

    public void setUserType(Integer userType) {
        this.userType = userType;
    }
}
