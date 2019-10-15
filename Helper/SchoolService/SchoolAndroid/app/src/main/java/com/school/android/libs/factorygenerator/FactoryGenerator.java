package com.school.android.libs.factorygenerator;

import com.school.android.libs.factorygenerator.interfaces.CreateObject;
import com.school.android.libs.factorygenerator.interfaces.WhenFactory;

import java.util.ArrayList;
import java.util.List;

public class FactoryGenerator<T, W, A> {

    public class FactoryPosition<T, W, A> {

        private CreateObject<T, A> createObject;
        private WhenFactory<W> whenFactory;

        public FactoryPosition(CreateObject<T, A> createObject) {
            this.createObject = createObject;
        }

        public void when(WhenFactory<W> whenFactory) {
            this.whenFactory = whenFactory;
        }

        public void when(final W when) {
            this.whenFactory = new WhenFactory<W>() {
                @Override
                public boolean is(W w) {
                    return w.equals(when);
                }
            };
        }

        public boolean is(W w) {
            return whenFactory.is(w);
        }
    }

    private List<FactoryPosition<T, W, A>> factoryPositions = new ArrayList<>();

    public FactoryPosition<T, W, A> add(CreateObject<T, A> createObject) {
        FactoryPosition<T, W, A> position = new FactoryPosition<>(createObject);
        factoryPositions.add(position);
        return position;
    }

    public T create(W w) {
        return create(w, null);
    }

    public T create(W w, A a) {
        for (int i = 0; i < factoryPositions.size(); i++) {
            if (factoryPositions.get(i).is(w))
                return factoryPositions.get(i).createObject.create(a);
        }
        return null;
    }


}
