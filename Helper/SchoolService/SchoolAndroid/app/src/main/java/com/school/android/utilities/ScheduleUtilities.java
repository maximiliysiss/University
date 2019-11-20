package com.school.android.utilities;

import com.school.android.models.network.input.Schedule;

import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

public class ScheduleUtilities {
    public static Map<Integer, List<Schedule>> sort(List<Schedule> schedules) {
        return schedules.stream().collect(Collectors.groupingBy(x -> x.getDayOfWeek()));
    }
}
