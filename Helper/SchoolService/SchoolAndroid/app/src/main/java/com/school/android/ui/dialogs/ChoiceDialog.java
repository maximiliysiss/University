package com.school.android.ui.dialogs;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;

import com.school.android.R;

public class ChoiceDialog {
    DialogAction dialogAction;
    Context context;

    public ChoiceDialog(Context context, DialogAction dialogAction) {
        this.dialogAction = dialogAction;
        this.context = context;
    }

    public void show() {
        AlertDialog.Builder builder = new AlertDialog.Builder(context).setTitle(R.string.sure)
                .setPositiveButton(R.string.yes, (dialog, which) -> dialogAction.action())
                .setNegativeButton(R.string.no, (dialog, which) -> {
                });
        builder.create().show();
    }
}
