package com.example.laboratory_2;

import android.os.Bundle;
import android.text.InputFilter;
import android.text.Spanned;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;

import com.example.laboratory_2.services.Converter;
import com.example.laboratory_2.services.Parser;

public class MainActivity extends AppCompatActivity {


    EditText editText;
    Double first;
    String operation;

    Parser parser = new Parser();
    Converter converter = new Converter();

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        editText = findViewById(R.id.text);


        InputFilter filter = new InputFilter() {
            @Override
            public CharSequence filter(CharSequence charSequence, int i, int i1, Spanned spanned, int i2, int i3) {
                return parser.getNumberFilter().FilterSeq(charSequence);
            }
        };

        editText.setFilters(new InputFilter[]{filter});
    }

    public void actionClick(View view) {
        Button button = (Button) view;

        String text = button.getText().toString().trim();
        Integer number = converter.tryParseInt(text);
        if (number != null) {
            editText.append(number.toString());
            return;
        }

        String fullText = editText.getText().toString();

        switch (text) {
            case "+":
            case "-":
            case "/":
            case "*":
                operation = text;
                if (first != null) {
                    first = parser.getOperation(operation).operation(first, parser.parsing(fullText));
                } else {
                    first = parser.parsing(fullText);
                }
                editText.setText("");
                break;
            case "C":
                first = null;
                operation = null;
                editText.setText("");
                break;
            case "=":
                if (first != null || editText.getText().toString().trim().length() > 0) {
                    Double res = 0.0;
                    if (operation != null)
                        res = parser.getOperation(operation).operation(first, parser.parsing(fullText));
                    else
                        res = parser.parsing(fullText);
                    editText.setText(res.toString());
                    first = null;
                }
                break;
        }
    }
}
