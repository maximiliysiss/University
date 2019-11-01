package com.school.android.utilities;

import com.school.android.models.network.input.Lesson;
import com.school.android.models.network.input.Mark;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.SortedSet;
import java.util.TreeSet;

import static java.util.stream.Collectors.groupingBy;

public class MarkUtilities {

    public static Map<Integer, List<List<Mark>>> getData(List<Mark> marks) {
        Map<Integer, List<Mark>> byDays = marks.stream().collect(groupingBy(m -> m.getSchedule().getDayOfWeek()));
        Map<Integer, List<List<Mark>>> data = new HashMap<>();

        DayUtils.getDays().forEach((k, v) -> {

            if (byDays.containsKey(k)) {
                List<List<Mark>> rec = new ArrayList<>();
                Map<Integer, List<Mark>> groupByLesson = byDays.get(k).stream().collect(groupingBy(m -> m.getSchedule().getId()));
                groupByLesson.forEach((ki, li) -> {
                    li.sort((o1, o2) -> o1.getSchedule().getLessonNumber().compareTo(o2.getSchedule().getLessonNumber()));
                    rec.add(li);
                });
                rec.sort((o1, o2) -> {
                    Mark m1 = o1.get(0);
                    Mark m2 = o2.get(0);
                    return m1.getSchedule().getLessonNumber().compareTo(m2.getSchedule().getLessonNumber());
                });
                data.put(k, rec);
            } else
                data.put(k, new ArrayList<>());

        });

        return data;
    }

}
