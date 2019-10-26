package com.application.library.ui.adapters.recyclerviews.ViewHolder;

import android.content.Intent;
import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.application.library.R;
import com.application.library.app.App;
import com.application.library.network.models.addons.UserRole;
import com.application.library.network.models.input.Book;
import com.application.library.ui.BookActivity;

public class BookViewHolder extends RecyclerViewHolder<Book> {

    TextView name;
    TextView author;
    TextView price;

    public BookViewHolder(@NonNull View itemView) {
        super(itemView);

        name = itemView.findViewById(R.id.name);
        author = itemView.findViewById(R.id.author);
        price = itemView.findViewById(R.id.price);
    }

    @Override
    public void onClick() {

        UserRole userRole = UserRole.values()[App.getUserContext().getUserRole()];
        if (userRole == UserRole.Admin) {
            Intent intent = new Intent(this.getActivity(), BookActivity.class);
            intent.putExtra("book", object);
            getActivity().startActivity(intent);
        }
    }

    @Override
    public void setObject(Book object) {
        super.setObject(object);

        name.setText(object.getName());
        author.setText(object.getAuthor());
        price.setText(String.valueOf(object.getPrice()));
    }
}
