package com.school.android.models.network.input;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;
import com.school.android.models.network.FragmentModel;

import java.util.List;

public class RiskGroup implements FragmentModel {
    @SerializedName("id")
    @Expose
    private Integer id;
    @SerializedName("name")
    @Expose
    private String name;
    @SerializedName("childInRiskGroups")
    @Expose
    private List<ChildInRiskGroup> childInRiskGroups = null;

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

    public List<ChildInRiskGroup> getChildInRiskGroups() {
        return childInRiskGroups;
    }

    public void setChildInRiskGroups(List<ChildInRiskGroup> childInRiskGroups) {
        this.childInRiskGroups = childInRiskGroups;
    }
}
