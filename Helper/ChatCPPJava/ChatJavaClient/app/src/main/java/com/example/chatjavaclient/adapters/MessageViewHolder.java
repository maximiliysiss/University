package com.example.chatjavaclient.adapters;

import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.example.chatjavaclient.R;

public class MessageViewHolder extends AbstractViewHolder<String> {

    TextView textView;

    public MessageViewHolder(@NonNull View itemView) {
        super(itemView);
        textView = itemView.findViewById(R.id.msg);
    }

    @Override
    public void bind(String val) {
        textView.setText(val);
    }
}
