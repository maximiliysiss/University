package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.school.android.models.network.FragmentModel;

public class User implements FragmentModel {
    @SerializedName("id")
    @Expose
    protected Integer id = 0;
    @SerializedName("name")
    @Expose
    protected String name = "";
    @SerializedName("surname")
    @Expose
    protected String surname = "";
    @SerializedName("secondName")
    @Expose
    protected String secondName = "";
    @SerializedName("birthday")
    @Expose
    protected String birthday = "";
    @SerializedName("userType")
    @Expose
    protected Integer userType = 0;
    @SerializedName("phone")
    @Expose
    protected String phone = "";
    @SerializedName("passport")
    @Expose
    protected String passport = "";
    @SerializedName("email")
    @Expose
    protected String email = "";

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassport() {
        return passport;
    }

    public void setPassport(String passport) {
        this.passport = passport;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
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
