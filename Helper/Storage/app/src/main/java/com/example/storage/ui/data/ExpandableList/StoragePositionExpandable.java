package com.example.storage.ui.data.ExpandableList;

import android.view.View;
import android.widget.TextView;

import com.example.storage.R;
import com.example.storage.data.models.StoragePosition;

import java.util.HashMap;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Опеределение выпадающего списка для позиций
 */
public class StoragePositionExpandable implements ExpandableListConstructor<String, StoragePosition> {

    /**
     * Данные
     */
    HashMap<String, List<StoragePosition>> stringListHashMap;

    public StoragePositionExpandable(HashMap<String, List<StoragePosition>> stringListHashMap) {
        this.stringListHashMap = stringListHashMap;
    }

    @Override
    public List<String> getHeaders() {
        return stringListHashMap.keySet().stream().collect(Collectors.toList());
    }

    @Override
    public HashMap<String, List<StoragePosition>> getChildren() {
        return stringListHashMap;
    }

    @Override
    public void constructGroup(View v, String elem, int index) {
        TextView textView = v.findViewById(R.id.name);
        textView.setText(elem);
    }

    @Override
    public void constructChild(View v, StoragePosition elem, int index, int childIndex) {
        TextView sum = v.findViewById(R.id.sum);
        sum.setText(String.valueOf(elem.getSum()));
    }

    @Override
    public int getChildLayout() {
        return R.layout.storage_list_element;
    }

    @Override
    public int getGroupLayout() {
        return R.layout.storage_list_header;
    }
}
