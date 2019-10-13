package com.example.storage.data.daos;

import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.Update;

/**
 * Интерфейс шаблон операций для моделей в БД
 * @param <T>
 */
@Dao
public interface DaoCrudTemplate<T> {

    /**
     * Добавить
     * @param ts
     */
    @Insert
    void insertAll(T... ts);

    /**
     * Удалить
     * @param t
     */
    @Delete
    void delete(T t);

    /**
     * Обновить
     * @param elem
     */
    @Update
    void update(T elem);

}
