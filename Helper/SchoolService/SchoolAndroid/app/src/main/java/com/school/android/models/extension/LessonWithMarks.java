package com.school.android.models.extension;

import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;

import java.util.List;

public class LessonWithMarks {
    private Lesson lesson;

    private List<Mark> markList;

    public LessonWithMarks(Lesson lesson, List<Mark> markList) {
        this.lesson = lesson;
        this.markList = markList;
    }

    public Lesson getLesson() {
        return lesson;
    }

    public void setLesson(Lesson lesson) {
        this.lesson = lesson;
    }

    public List<Mark> getMarkList() {
        return markList;
    }

    public void setMarkList(List<Mark> markList) {
        this.markList = markList;
    }
}
