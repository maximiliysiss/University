package com.application.autostation.ui.activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.telephony.PhoneNumberFormattingTextWatcher;
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

/**
 * Форма оплаты
 */
public class PayActivity extends AppCompatActivity {

    /**
     * Поля
     */
    EditText card;
    EditText count;
    /**
     * Расписание
     */
    Schedule schedule;

    /**
     * Создание формы
     *
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pay);
    }

    /**
     * Запуск формы
     */
    @Override
    protected void onStart() {
        super.onStart();

        /**
         * Получим расписание
         */
        schedule = (Schedule) getIntent().getExtras().getSerializable(getString(R.string.schedule_model));

        TextView price = findViewById(R.id.price);
        card = findViewById(R.id.card);
        count = findViewById(R.id.count);
        /**
         * Когда меняем количество, то меняем и сумму
         */
        count.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void afterTextChanged(Editable s) {
                if (s.toString().trim().length() == 0) {
                    s.append("0");
                }
                price.setText(String.valueOf(Integer.parseInt(s.toString()) * schedule.getPrice()));
            }
        });
        count.setText("1");
    }

    /**
     * Нельзя вернуться назад
     */
    @Override
    public void onBackPressed() {
    }

    /**
     * Нажатие кнопки оплата
     *
     * @param view
     */
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

    /**
     * Нажатие кнопки назад
     *
     * @param view
     */
    public void cancel(View view) {
        startActivity(new Intent(this, UserActivity.class));
    }
}
