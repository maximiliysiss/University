package com.example.storage.data.models;

import androidx.room.Embedded;
import androidx.room.Relation;

import java.util.List;

/**
 * Группа с позициями
 */
public class StorageInGroups {
    @Embedded
    private  Group group;
    @Relation(parentColumn = "id", entityColumn = "groupId", entity =  StoragePosition.class)
    private List<StoragePosition> storagePositionList;

    public StorageInGroups(Group group, List<StoragePosition> storagePositionList) {
        this.group = group;
        this.storagePositionList = storagePositionList;
    }

    public StorageInGroups() {
    }

    public Group getGroup() {
        return group;
    }

    public void setGroup(Group group) {
        this.group = group;
    }

    public List<StoragePosition> getStoragePositionList() {
        return storagePositionList;
    }

    public void setStoragePositionList(List<StoragePosition> storagePositionList) {
        this.storagePositionList = storagePositionList;
    }
}
