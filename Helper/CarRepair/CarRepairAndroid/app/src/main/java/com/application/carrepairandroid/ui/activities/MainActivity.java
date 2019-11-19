package com.application.carrepairandroid.ui.activities;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;

import com.application.carrepairandroid.R;
import com.application.carrepairandroid.application.App;
import com.application.carrepairandroid.network.callbacks.CallbackAction;
import com.application.carrepairandroid.network.callbacks.UniversalCallback;
import com.application.carrepairandroid.network.models.input.Service;
import com.application.carrepairandroid.ui.adapters.recyclerview.RecyclerViewAdapter;
import com.application.carrepairandroid.ui.adapters.recyclerview.ViewHolders.ServiceViewHolder;

import java.util.List;

public class MainActivity extends AppCompatActivity {

    RecyclerView recyclerView;

    @Override
    public void onBackPressed() {

        if (App.getUserContext() != null) {
            AlertDialog.Builder builder = new AlertDialog.Builder(this).setTitle("Сменить пользователя?")
                    .setNegativeButton("Нет", (dialog, which) -> {
                    })
                    .setPositiveButton("Да", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                            App.signOut();
                            startActivity(new Intent(MainActivity.this, LoginActivity.class));
                        }
                    });

            builder.create().show();
        } else
            super.onBackPressed();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        recyclerView = findViewById(R.id.data);
        recyclerView.setLayoutManager(new LinearLayoutManager(getBaseContext()));
        App.getServiceRetrofit().getModels().enqueue(new UniversalCallback<>(getBaseContext(), new CallbackAction<List<Service>>() {
            @Override
            public void process(List<Service> body) {
                recyclerView.setAdapter(new RecyclerViewAdapter(body, R.layout.recycler_service, x -> new ServiceViewHolder(x, getString(R.string.model_service))));
            }
        }));

        if (App.getUserContext() == null) {
            findViewById(R.id.add).setVisibility(View.INVISIBLE);
        }
    }

    public void add(View view) {
        Intent intent = new Intent(this, ServiceActivity.class);
        intent.putExtra(getString(R.string.model_service), new Service());
        startActivity(intent);
    }
}
