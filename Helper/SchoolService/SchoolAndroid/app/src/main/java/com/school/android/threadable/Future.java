package com.school.android.threadable;

import java.io.IOException;

public class Future<T> extends Thread {

    ThreadResult<T> threadResult;
    T result;

    public Future(ThreadResult<T> threadResult) {
        this.threadResult = threadResult;
        this.start();
    }

    @Override
    public void run() {
        super.run();
        try {
            result = threadResult.get();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

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
