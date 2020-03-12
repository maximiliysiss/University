package com.example.testangryandroid.network.models;

public class ResultModel {
    private String name;
    private Float result;

    public ResultModel(String name, Float result) {
        this.name = name;
        this.result = result;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Float getResult() {
        return result;
    }

    public void setResult(Float result) {
        this.result = result;
    }
}
