package com.application.autostation.network.models.input;

import com.application.autostation.ui.models.Model;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Schedule implements Model {
    @SerializedName("id")
    @Expose
    private Integer id = 0;
    @SerializedName("time")
    @Expose
    private String time;
    @SerializedName("distance")
    @Expose
    private Integer distance;
    @SerializedName("dayOfWeek")
    @Expose
    private Integer dayOfWeek;
    @SerializedName("fromId")
    @Expose
    private Integer fromId;
    @SerializedName("from")
    @Expose
    private Point from;
    @SerializedName("to")
    @Expose
    private Point to;
    @SerializedName("toId")
    @Expose
    private Integer toId;
    @SerializedName("price")
    @Expose
    private Double price;

    public Integer getFromId() {
        return fromId;
    }

    public void setFromId(Integer fromId) {
        this.fromId = fromId;
    }

    public Integer getToId() {
        return toId;
    }

    public void setToId(Integer toId) {
        this.toId = toId;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getTime() {
        return time;
    }

    public void setTime(String time) {
        this.time = time;
    }

    public Integer getDistance() {
        return distance;
    }

    public void setDistance(Integer distance) {
        this.distance = distance;
    }

    public Integer getDayOfWeek() {
        return dayOfWeek;
    }

    public void setDayOfWeek(Integer dayOfWeek) {
        this.dayOfWeek = dayOfWeek;
    }

    public Point getFrom() {
        return from;
    }

    public void setFrom(Point from) {
        this.from = from;
    }

    public Point getTo() {
        return to;
    }

    public void setTo(Point to) {
        this.to = to;
    }

    public Double getPrice() {
        return price;
    }

    public void setPrice(Double price) {
        this.price = price;
    }
}
