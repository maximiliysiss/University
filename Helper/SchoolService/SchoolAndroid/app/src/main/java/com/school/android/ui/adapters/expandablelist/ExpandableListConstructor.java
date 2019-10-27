package com.school.android.ui.adapters.expandablelist;

import android.view.View;

import java.util.HashMap;
import java.util.List;

public interface ExpandableListConstructor<T, T1> {
    List<T> getHeaders();

    /*Получить Child элементы в виде HashMap*/
    HashMap<T, List<T1>> getChildren();

    /*Создать заголовок*/
    void constructGroup(View v, T elem, int index);

    /*Создать Child элемент*/
    void constructChild(View v, T1 elem, int index, int childIndex);

    /*Получить слой для Child элемент*/
    int getChildLayout();

    /*Получить слой заголовка*/
    int getGroupLayout();
}