package com.school.android.ui.spinner;

public interface Observable {
    void AddObserver(Observer observer);
    void RemoveObserver(Observer observer);
    void notifyObservers();
}
