package com.school.android.ui.adapters.edittext;

import android.text.Editable;
import android.text.TextWatcher;
import android.widget.EditText;

import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.Locale;

public class EditTextListener implements TextWatcher {

    EditText editText;

    public EditTextListener(EditText editText) {
        this.editText = editText;
    }

    @Override
    public void beforeTextChanged(CharSequence s, int start, int count, int after) {

    }

    @Override
    public void onTextChanged(CharSequence s, int start, int before, int count) {
    }

    @Override
    public void afterTextChanged(Editable s) {

        try {
            String originalString = s.toString();
            originalString = originalString.replaceAll("[^0-9]", "");

            Long longval;
            longval = Long.parseLong(originalString);

            DecimalFormat formatter = (DecimalFormat) NumberFormat.getInstance(Locale.US);
            formatter.applyPattern("#### / ######");
            String formattedString = formatter.format(longval);
            editText.setText(formattedString);
        } catch (NumberFormatException nfe) {
            nfe.printStackTrace();
        }
    }
}
