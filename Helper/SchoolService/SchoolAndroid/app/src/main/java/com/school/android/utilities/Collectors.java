package com.school.android.utilities;

import java.util.ArrayList;
import java.util.List;

public class Collectors {

    public static <T> List<T> getList(T obj) {
        ArrayList<T> ts = new ArrayList<>();
        ts.add(obj);
        return ts;
    }
    
}
