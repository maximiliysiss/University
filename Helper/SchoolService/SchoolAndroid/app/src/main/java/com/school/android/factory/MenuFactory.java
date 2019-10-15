package com.school.android.factory;

import com.school.android.R;
import com.school.android.libs.factorygenerator.FactoryGenerator;
import com.school.android.libs.factorygenerator.interfaces.CreateObject;
import com.school.android.libs.factorygenerator.interfaces.WhenFactory;
import com.school.android.models.extension.UserType;
import com.school.android.models.network.input.Teacher;
import com.school.android.models.network.input.User;

public class MenuFactory {

    private static FactoryGenerator<Integer, User, Void> menuFactory;

    public static FactoryGenerator<Integer, User, Void> getMenuFactory() {
        if (menuFactory != null)
            return menuFactory;

        menuFactory = new FactoryGenerator<>();
        menuFactory.add(new CreateObject<Integer, Void>() {
            @Override
            public Integer create(Void aVoid) {
                return R.menu.bottom_nav_menu_admin;
            }
        }).when(new WhenFactory<User>() {
            @Override
            public boolean is(User user) {
                return user.getUserType() == UserType.Admin.ordinal();
            }
        });

        menuFactory.add(new CreateObject<Integer, Void>() {
            @Override
            public Integer create(Void aVoid) {
                return R.menu.bottom_nav_menu_superteacher;
            }
        }).when(new WhenFactory<User>() {
            @Override
            public boolean is(User user) {
                if (user instanceof Teacher)
                    return ((Teacher) user).getIsClassWork() && user.getUserType() == UserType.Teacher.ordinal();
                return false;
            }
        });

        menuFactory.add(new CreateObject<Integer, Void>() {
            @Override
            public Integer create(Void aVoid) {
                return R.menu.bottom_nav_menu_student;
            }
        }).when(new WhenFactory<User>() {
            @Override
            public boolean is(User user) {
                return user.getUserType() == UserType.Student.ordinal();
            }
        });

        menuFactory.add(new CreateObject<Integer, Void>() {
            @Override
            public Integer create(Void aVoid) {
                return R.menu.bottom_nav_menu_social;
            }
        }).when(new WhenFactory<User>() {
            @Override
            public boolean is(User user) {
                return user.getUserType() == UserType.Social.ordinal();
            }
        });

        menuFactory.add(new CreateObject<Integer, Void>() {
            @Override
            public Integer create(Void aVoid) {
                return R.menu.bottom_nav_menu_knowledgeteacher;
            }
        }).when(new WhenFactory<User>() {
            @Override
            public boolean is(User user) {
                return user.getUserType() == UserType.KnowledgeTeacher.ordinal();
            }
        });

        menuFactory.add(new CreateObject<Integer, Void>() {
            @Override
            public Integer create(Void aVoid) {
                return R.menu.bottom_nav_menu_jobteacher;
            }
        }).when(new WhenFactory<User>() {
            @Override
            public boolean is(User user) {
                return user.getUserType() == UserType.JobTeacher.ordinal();
            }
        });

        menuFactory.add(new CreateObject<Integer, Void>() {
            @Override
            public Integer create(Void aVoid) {
                return R.menu.bottom_nav_menu_teacher;
            }
        }).when(new WhenFactory<User>() {
            @Override
            public boolean is(User user) {
                return user.getUserType() == UserType.Teacher.ordinal();
            }
        });
        
        return menuFactory;
    }
}
