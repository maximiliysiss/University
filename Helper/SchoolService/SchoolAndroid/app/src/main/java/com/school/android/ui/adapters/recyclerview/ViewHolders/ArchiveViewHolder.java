package com.school.android.ui.adapters.recyclerview.ViewHolders;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.view.View;

import androidx.annotation.NonNull;

import com.school.android.R;
import com.school.android.application.App;
import com.school.android.network.classes.UniversalCallback;

public class ArchiveViewHolder extends ChildrenViewHolder {

    public ArchiveViewHolder(@NonNull View itemView, String modelName) {
        super(itemView, modelName);
    }

    @Override
    public void onClick() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity()).setTitle(getString(R.string.from_archive))
                .setNegativeButton(getString(R.string.no), (dialog, which) -> {
                }).setPositiveButton(getString(R.string.yes), (dialog, which) -> App.getChildrenRetrofit().archive(object.getId()).enqueue(new UniversalCallback<>(getRealActivity(), x -> {
                    getRealActivity().openFragment(R.id.navigation_archive);
                })));
        builder.create().show();
    }
}
