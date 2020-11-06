package com.example.plantsdictionary.ui.controls.ui;

import android.view.View;
import android.widget.TextView;

import androidx.annotation.NonNull;

import com.example.plantsdictionary.R;
import com.example.plantsdictionary.ui.controls.recyclerview.viewholder.CardViewHolder;
import com.example.plantsdictionary.ui.controls.ui.models.ActionViewModel;
import com.example.plantsdictionary.ui.interfaces.ActivityNavigator;

public class ActionRecyclerViewHolder extends CardViewHolder<ActionViewModel> {

    TextView actionTitle;

    /**
     * @param itemView
     */
    public ActionRecyclerViewHolder(@NonNull View itemView) {
        super(itemView);
        actionTitle = itemView.findViewById(R.id.action_title);
    }

    @Override
    public void setObj(ActionViewModel obj) {
        super.setObj(obj);
        actionTitle.setText(obj.getCaption());
    }

    @Override
    public void click() {
        super.click();

        ActivityNavigator activityNavigator = (ActivityNavigator) getActivity();
        activityNavigator.navigateTo(getNavigateByName(obj.getNavigateTo()));
    }
}
