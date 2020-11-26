package com.example.plantsdictionary.infrastructure.threads;

import java.util.function.Consumer;

/**
 * Упрощенный Future без ожидания результата, но упрощает использование потоков
 */
public class Job extends Thread {

    /**
     * Код который нужно исполнить
     */
    private Consumer consumer;

    public Job(Consumer consumer) {
        this.consumer = consumer;
        this.start();
    }

    @Override
    public void run() {
        super.run();
        consumer.accept(null);
    }
}
