package com.application.autostation.ui.statistics;

import android.os.Build;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.fragment.app.Fragment;

import com.application.autostation.R;
import com.application.autostation.app.App;
import com.application.autostation.network.callbacks.UniversalCallback;
import com.application.autostation.network.models.input.Statistics;
import com.application.autostation.utilities.MonthYear;
import com.github.mikephil.charting.charts.BarChart;
import com.github.mikephil.charting.components.Description;
import com.github.mikephil.charting.data.BarData;
import com.github.mikephil.charting.data.BarDataSet;
import com.github.mikephil.charting.data.BarEntry;
import com.github.mikephil.charting.formatter.PercentFormatter;
import com.github.mikephil.charting.utils.ColorTemplate;

import java.time.YearMonth;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Comparator;
import java.util.Date;
import java.util.List;

public class StatisticsFragment extends Fragment {

    BarChart barChart;
    MonthYear monthYear;
    TextView info;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_statistics, container, false);
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    public void onStart() {
        super.onStart();

        info = getView().findViewById(R.id.info);
        barChart = getView().findViewById(R.id.bar);
        ImageView left = getView().findViewById(R.id.left);
        ImageView right = getView().findViewById(R.id.right);

        left.setOnClickListener(v -> {
            monthYear.addMonth(-1);
            loadData();
        });

        right.setOnClickListener(v -> {
            monthYear.addMonth(1);
            loadData();
        });

        Date date = Calendar.getInstance().getTime();
        monthYear = new MonthYear(date.getYear() + 1900, date.getMonth() + 1);
        loadData();
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    private void loadData() {
        App.getBuyingsRetrofit().getStatistics(monthYear.getYear(), monthYear.getMonth()).enqueue(new UniversalCallback<>(getContext(), x -> {

            ArrayList<BarEntry> entries = new ArrayList<>();

            YearMonth yearMonthObject = YearMonth.of(monthYear.getYear(), monthYear.getMonth());
            int daysInMonth = yearMonthObject.lengthOfMonth();
            for (int i = 0; i < daysInMonth; i++) {
                entries.add(new BarEntry((float) i, 0.0f));
            }

            for (Statistics statistics : x) {
                entries.set(statistics.getDay() - 1, new BarEntry(statistics.getDay(), statistics.getSum()));
            }

            BarDataSet pieDataSet = new BarDataSet(entries, "");
            pieDataSet.setColors(ColorTemplate.VORDIPLOM_COLORS);
            Description description = barChart.getDescription();
            description.setText("");
            info.setText(String.valueOf(monthYear.getMonth()) + "/" + monthYear.getYear());
            barChart.setData(new BarData(pieDataSet));
            barChart.invalidate();
        }));
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void left(View view) {
        monthYear.addMonth(-1);
        loadData();
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void right(View view) {
        monthYear.addMonth(1);
        loadData();
    }
}