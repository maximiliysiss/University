package com.application.autostation.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.Buying;
import com.application.autostation.network.models.input.Schedule;

public class PayActivity extends AppCompatActivity {

    EditText card;
    EditText count;
    Schedule schedule;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pay);
    }

    @Override
    protected void onStart() {
        super.onStart();

        schedule = (Schedule) getIntent().getExtras().getSerializable(getString(R.string.schedule_model));

        TextView price = findViewById(R.id.price);
        card = findViewById(R.id.card);
        count = findViewById(R.id.count);
        count.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void afterTextChanged(Editable s) {
                price.setText(String.valueOf(Integer.parseInt(s.toString()) * schedule.getPrice()));
            }
        });
        count.setText("1");
    }

    @Override
    public void onBackPressed() {
    }

    public void pay(View view) {
        String cardString = card.getText().toString().trim();
        String countString = count.getText().toString().trim();

        if (cardString.length() == 0 || countString.length() == 0) {
            Toast.makeText(getBaseContext(), "Заполните поля", Toast.LENGTH_SHORT).show();
            return;
        }

        Buying buying = new Buying();
        buying.setCount(Integer.parseInt(countString));
        buying.setScheduleId(schedule.getId());

        App.getBuyingsRetrofit().create(buying).enqueue(new UniversalCallback<>(getBaseContext(), x -> {
            Toast.makeText(getBaseContext(), "Оплата прошла успешно", Toast.LENGTH_SHORT).show();
            startActivity(new Intent(this, UserActivity.class));
        }));
    }

    public void cancel(View view) {
        startActivity(new Intent(this, UserActivity.class));
    }
}
