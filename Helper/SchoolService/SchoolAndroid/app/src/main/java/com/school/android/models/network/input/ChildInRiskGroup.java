package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class ChildInRiskGroup {
    @SerializedName("id")
    @Expose
    private Integer id;
    @SerializedName("riskGroupId")
    @Expose
    private Integer riskGroupId;
    @SerializedName("childId")
    @Expose
    private Integer childId;
    @SerializedName("child")
    @Expose
    private Children child;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getRiskGroupId() {
        return riskGroupId;
    }

    public void setRiskGroupId(Integer riskGroupId) {
        this.riskGroupId = riskGroupId;
    }

    public Integer getChildId() {
        return childId;
    }

    public void setChildId(Integer childId) {
        this.childId = childId;
    }

    public Children getChild() {
        return child;
    }

    public void setChild(Children child) {
        this.child = child;
    }

}
