package com.example.plantsdictionary.ui.interfaces;

/**
 * Activity с Toolbar
 */
public interface ToolbarActivity {
    /**
     * Обновить заголовок
     *
     * @param title
     */
    void updateTitle(String title);

    /**
     * Получить текущий заголовок
     *
     * @return
     */
    String getToolbarTitle();
}
