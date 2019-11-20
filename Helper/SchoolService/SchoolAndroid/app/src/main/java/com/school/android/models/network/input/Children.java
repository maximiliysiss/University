package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.school.android.models.extension.UserType;

public class Children extends User {

    public Children() {
        userType = UserType.Student.ordinal();
    }

    @SerializedName("classId")
    @Expose
    private Integer classId;

    @SerializedName("class")
    @Expose
    private Class _class;
    @SerializedName("isArchive")
    @Expose
    private Boolean isArchive;

    public Integer getClassId() {
        return classId;
    }

    public void setClassId(Integer classId) {
        this.classId = classId;
    }

    public Class getClass_() {
        return _class;
    }

    public void setClass_(Class _class) {
        this._class = _class;
    }

    public Boolean getIsArchive() {
        return isArchive;
    }

    public void setIsArchive(Boolean isArchive) {
        this.isArchive = isArchive;
    }

}
