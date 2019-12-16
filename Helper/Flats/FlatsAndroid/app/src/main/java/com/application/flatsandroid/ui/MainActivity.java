package com.application.flatsandroid.ui;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.application.flatsandroid.R;
import com.application.flatsandroid.app.App;
import com.application.flatsandroid.network.callbacks.UniversalCallback;
import com.application.flatsandroid.network.models.input.Realty;
import com.application.flatsandroid.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.application.flatsandroid.ui.adapters.recyclerview.ViewHolder.RealtyViewHolder;

public class MainActivity extends AppCompatActivity {

    RecyclerView recyclerView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        recyclerView = findViewById(R.id.models);
        recyclerView.setLayoutManager(new LinearLayoutManager(getBaseContext()));
        App.getRealtyService().getAll().enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            recyclerView.setAdapter(new RecyclerViewAdapter(x, R.layout.recycler_item, v -> new RealtyViewHolder(v)));
        }));

        Button add = findViewById(R.id.add);

        if (App.getRole() == 1)
            add.setVisibility(View.INVISIBLE);

    }

    public void addNew(View view) {
        Intent intent = new Intent(this, RealtyActivity.class);
        intent.putExtra("model", new Realty());
        startActivity(intent);
    }
}
