package com.example.plantsdictionary.infrastructure.threads;

/**
 * Класс для синхронного выполнения кода + получения результата
 * @param <T>
 */
public class Future<T> extends Thread {

    /**
     * Код делегата
     */
    ThreadResult<T> threadResult;
    /**
     * Результат
     */
    T result;

    public Future(ThreadResult<T> threadResult) {
        this.threadResult = threadResult;
        this.start();
    }

    @Override
    public void run() {
        super.run();
        result = threadResult.get();
    }

    /**
     * Дождемся завершения и вернем результат
     * @return
     */
    public T get() {
        try {
            this.join();
            return result;
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return null;
    }
}
