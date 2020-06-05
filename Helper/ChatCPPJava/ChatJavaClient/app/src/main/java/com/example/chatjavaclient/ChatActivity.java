package com.example.chatjavaclient;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Intent;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import com.example.chatjavaclient.adapters.MessageViewHolder;
import com.example.chatjavaclient.adapters.SimpleViewAdapter;
import com.example.chatjavaclient.adapters.interfaces.BuilderViewHolder;
import com.example.chatjavaclient.chatsocket.ChatSocket;

import org.w3c.dom.Text;

import java.util.ArrayList;

public class ChatActivity extends AppCompatActivity {

    ChatSocket chatSocket;
    EditText messageEditText;
    SimpleViewAdapter<MessageViewHolder, String> adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chat);

        RecyclerView recyclerView = findViewById(R.id.messages);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new SimpleViewAdapter<>(new ArrayList<>(), parent -> new MessageViewHolder(parent), R.layout.message_item);
        recyclerView.setAdapter(adapter);

        messageEditText = findViewById(R.id.message);

    }

    @Override
    protected void onStart() {
        super.onStart();

        chatSocket = new ChatSocket(getIntent().getStringExtra("userName"), getString(R.string.ip), Integer.parseInt(getString(R.string.port)), this);
        chatSocket.start();
    }

    public void addMessage(String msg) {
        runOnUiThread(() -> adapter.addItem(msg));
    }

    public void sendMessage(View view) {

        String msg = messageEditText.getText().toString().trim();
        if (msg.length() == 0)
            return;

        chatSocket.sendMessage(msg);
    }
}
