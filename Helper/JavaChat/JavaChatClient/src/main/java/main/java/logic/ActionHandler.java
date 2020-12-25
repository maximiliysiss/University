package main.java.logic;

/**
 * Универсальный Action для делегатов
 * @param <T>
 */
public interface ActionHandler<T> {
    void handle(T obj);
}
