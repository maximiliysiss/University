package com.application.library.ui.adapters.recyclerviews.ViewHolder;

import android.content.Intent;
import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.application.library.app.App;
import com.application.library.network.models.input.Book;
import com.application.library.ui.BookActivity;
import com.school.library.R;

public class BookViewHolder extends RecyclerViewHolder<Book> {

    TextView name;
    TextView price;

    public BookViewHolder(@NonNull View itemView) {
        super(itemView);

        name = itemView.findViewById(R.id.name);
        price = itemView.findViewById(R.id.price);
    }

    @Override
    public void onClick() {

        if (App.isAuth()) {
            Intent intent = new Intent(this.getActivity(), BookActivity.class);
            intent.putExtra("book", object);
            getActivity().startActivity(intent);
        }
    }

    @Override
    public void setObject(Book object) {
        super.setObject(object);

        name.setText(object.getName());
        price.setText(String.valueOf(object.getPrice()));
    }
}
