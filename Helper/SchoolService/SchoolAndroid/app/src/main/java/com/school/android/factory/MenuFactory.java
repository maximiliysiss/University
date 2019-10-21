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
        menuFactory.add(R.menu.bottom_nav_menu_admin).when(user -> user.getUserType() == UserType.Admin.ordinal());

        menuFactory.add(R.menu.bottom_nav_menu_superteacher).when(user -> {
            if (user instanceof Teacher)
                return ((Teacher) user).getIsClassWork() && user.getUserType() == UserType.Teacher.ordinal();
            return false;
        });

        menuFactory.add(R.menu.bottom_nav_menu_student).when(user -> user.getUserType() == UserType.Student.ordinal());

        menuFactory.add(R.menu.bottom_nav_menu_social).when(user -> user.getUserType() == UserType.Social.ordinal());

        menuFactory.add(R.menu.bottom_nav_menu_knowledgeteacher).when(user -> user.getUserType() == UserType.KnowledgeTeacher.ordinal());

        menuFactory.add(R.menu.bottom_nav_menu_jobteacher).when(user -> user.getUserType() == UserType.JobTeacher.ordinal());

        menuFactory.add(R.menu.bottom_nav_menu_teacher).when(user -> user.getUserType() == UserType.Teacher.ordinal());

        return menuFactory;
    }
}
