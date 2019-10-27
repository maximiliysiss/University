package com.school.android.ui.adapters.expandablelist.expandableconstructors;

import android.app.Activity;
import android.content.Context;
import android.content.ContextWrapper;
import android.view.View;

import com.school.android.ui.activity.ActivityFragmenter;
import com.school.android.ui.adapters.expandablelist.ExpandableListConstructor;

import java.util.HashMap;
import java.util.List;
import java.util.stream.Collectors;

public abstract class AbstractExpandableConstructor<T, T1> implements ExpandableListConstructor<T, T1> {

    protected HashMap<T, List<T1>> hashMap;

    protected int groupLayout;
    protected int childLayout;

    public AbstractExpandableConstructor(HashMap<T, List<T1>> hashMap, int groupLayout, int childLayout) {
        this.hashMap = hashMap;
        this.groupLayout = groupLayout;
        this.childLayout = childLayout;
    }

    @Override
    public List<T> getHeaders() {
        return hashMap.keySet().stream().collect(Collectors.toList());
    }

    @Override
    public HashMap<T, List<T1>> getChildren() {
        return hashMap;
    }

    @Override
    public int getChildLayout() {
        return childLayout;
    }

    @Override
    public int getGroupLayout() {
        return groupLayout;
    }

    protected Activity getActivity(View view) {
        Context context = view.getContext();
        while (context instanceof ContextWrapper) {
            if (context instanceof Activity) {
                return (Activity) context;
            }
            context = ((ContextWrapper) context).getBaseContext();
        }
        return null;
    }

    protected ActivityFragmenter getRealActivity(View v) {
        return (ActivityFragmenter) getActivity(v);
    }
}
