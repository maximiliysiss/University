package com.school.android.threadable;

import java.io.IOException;

public interface ThreadResult<T> {
    T get() throws IOException;
}
