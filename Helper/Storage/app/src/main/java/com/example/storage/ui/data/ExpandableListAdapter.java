package com.example.storage.ui.data;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;

import com.example.storage.ui.data.ExpandableList.ExpandableListConstructor;

import java.util.HashMap;
import java.util.List;

/**
 * Адаптер для выпадающих списков
 * @param <T>
 * @param <T1>
 * @param <Card>
 */
public class ExpandableListAdapter<T, T1, Card extends ExpandableListConstructor<T, T1>> extends BaseExpandableListAdapter {

    /**
     * Контекст
     */
    private Context context;
    /**
     * Данные
     */
    private HashMap<T, List<T1>> data;
    /**
     * Заголовки
     */
    private List<T> headers;
    /**
     * Конструктор для элементов
     */
    Card card;

    public ExpandableListAdapter(Context context, Card card) {
        this.context = context;
        this.card = card;
        this.data = card.getChildren();
        this.headers = card.getHeaders();
    }

    /**
     * Количество групп
     * @return
     */
    @Override
    public int getGroupCount() {
        return data.keySet().size();
    }

    /**
     * Количество элементов в группе
     * @param groupPosition
     * @return
     */
    @Override
    public int getChildrenCount(int groupPosition) {
        return data.get(this.headers.get(groupPosition)).size();
    }

    /**
     * Получить группу
     * @param groupPosition
     * @return
     */
    @Override
    public Object getGroup(int groupPosition) {
        return this.headers.get(groupPosition);
    }

    /**
     * Получить элемент группы
     * @param groupPosition
     * @param childPosition
     * @return
     */
    @Override
    public Object getChild(int groupPosition, int childPosition) {
        return this.data.get(this.headers.get(groupPosition)).get(childPosition);
    }

    /**
     * Получить GroupId
     * @param groupPosition
     * @return
     */
    @Override
    public long getGroupId(int groupPosition) {
        return groupPosition;
    }

    /**
     * Получить ChildId
     * @param groupPosition
     * @param childPosition
     * @return
     */
    @Override
    public long getChildId(int groupPosition, int childPosition) {
        return childPosition;
    }

    @Override
    public boolean hasStableIds() {
        return false;
    }

    /**
     * Получить представление для группы
     * @param groupPosition
     * @param isExpanded
     * @param convertView
     * @param parent
     * @return
     */
    @Override
    public View getGroupView(int groupPosition, boolean isExpanded, View convertView, ViewGroup parent) {
        T listTitle = (T) getGroup(groupPosition);
        if (convertView == null) {
            LayoutInflater layoutInflater = (LayoutInflater) this.context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = layoutInflater.inflate(card.getGroupLayout(), null);
        }
        card.constructGroup(convertView, listTitle, groupPosition);
        return convertView;
    }

    /**
     * Получить представление элемента группы
     * @param groupPosition
     * @param childPosition
     * @param isLastChild
     * @param convertView
     * @param parent
     * @return
     */
    @Override
    public View getChildView(int groupPosition, int childPosition, boolean isLastChild, View convertView, ViewGroup parent) {
        T1 element = (T1) getChild(groupPosition, childPosition);
        if (convertView == null) {
            LayoutInflater layoutInflater = (LayoutInflater) this.context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = layoutInflater.inflate(card.getChildLayout(), null);
        }
        card.constructChild(convertView, element, groupPosition, childPosition);
        return convertView;
    }

    @Override
    public boolean isChildSelectable(int groupPosition, int childPosition) {
        return true;
    }
}
