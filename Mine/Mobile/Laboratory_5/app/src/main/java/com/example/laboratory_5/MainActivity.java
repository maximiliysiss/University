package com.example.laboratory_5;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.LiveData;
import androidx.room.Room;

import android.os.Bundle;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.widget.Toast;

import com.example.laboratory_5.databaseContext.AppDatabase;
import com.example.laboratory_5.models.Record;

import org.w3c.dom.Text;

import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;

public class MainActivity extends AppCompatActivity {

    TableLayout tableLayout;
    AppDatabase appDatabase;
    EditText nameTextBox;
    EditText emailTextBox;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        tableLayout = findViewById(R.id.data_table);
        nameTextBox = findViewById(R.id.name);
        emailTextBox = findViewById(R.id.email);
        appDatabase = Room.databaseBuilder(getApplicationContext(), AppDatabase.class, "laboratory_5-databases")
                .allowMainThreadQueries().build();
        reloadDataTable();
    }

    public TextView getColumnText(String text){
        TextView textView = new TextView(this);
        textView.setTextAlignment(View.TEXT_ALIGNMENT_CENTER);
        textView.setText(text);
        textView.setWidth(0);
        return textView;
    }

    public void reloadDataTable(){
        tableLayout.removeAllViews();
        List<Record> records = appDatabase.getRecordDao().getRecords();
        AtomicInteger i= new AtomicInteger(0);
        records.forEach(x->{
            TableRow tableRow = new TableRow(this);
            tableRow.setLayoutParams(new TableLayout.LayoutParams(TableLayout.LayoutParams.MATCH_PARENT, TableLayout.LayoutParams.WRAP_CONTENT));
            tableRow.addView(getColumnText(x.getName()));
            tableRow.addView(getColumnText(x.getEmail()));
            tableLayout.addView(tableRow, i.getAndIncrement());
        });
    }

    public void addNewRecord(View view) {
        String n = nameTextBox.getText().toString().trim();
        String e = emailTextBox.getText().toString().trim();

        if(n.length() == 0 || e.length()==0){
            Toast.makeText(this, "Name or email is empty", Toast.LENGTH_SHORT).show();
            return;
        }

        nameTextBox.setText("");
        emailTextBox.setText("");

        appDatabase.getRecordDao().insertAll(new Record(n,e));
        reloadDataTable();
    }
}
