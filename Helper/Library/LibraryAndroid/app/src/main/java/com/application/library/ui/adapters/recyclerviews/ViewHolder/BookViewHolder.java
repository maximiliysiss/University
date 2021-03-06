package com.application.library.ui.adapters.recyclerviews.ViewHolder;

import android.content.Intent;
import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.application.library.app.App;
import com.application.library.network.models.addons.UserRole;
import com.application.library.network.models.input.Book;
import com.application.library.ui.BookActivity;
import com.application.library.ui.BookReadActivity;
import com.school.library.R;

/**
 * Описание элемента RecyclerView для Книг
 */
public class BookViewHolder extends RecyclerViewHolder<Book> {

    /**
     * Поля для отображения
     */
    TextView name;
    TextView author;
    TextView pageCount;
    TextView year;

    public BookViewHolder(@NonNull View itemView) {
        super(itemView);

        name = itemView.findViewById(R.id.name);
        author = itemView.findViewById(R.id.author);
        pageCount = itemView.findViewById(R.id.count);
        year = itemView.findViewById(R.id.year);
    }

    /**
     * Обработка нажатия
     */
    @Override
    public void onClick() {

        UserRole userRole = UserRole.values()[App.getUserContext().getUserRole()];
        Intent intent;
        /**
         * Если админ, то открыть для изменения, иначе просмотр
         */
        if (userRole == UserRole.Admin)
            intent = new Intent(this.getActivity(), BookActivity.class);
        else
            intent = new Intent(this.getActivity(), BookReadActivity.class);
        intent.putExtra("book", object);
        getActivity().startActivity(intent);
    }

    /**
     * Заполнить поля объектом
     * @param object
     */
    @Override
    public void setObject(Book object) {
        super.setObject(object);

        name.setText(object.getName());
        author.setText(object.getAuthor());
        pageCount.setText(String.valueOf(object.getPagesCount()));
        year.setText(String.valueOf(object.getYear()));
    }
}
