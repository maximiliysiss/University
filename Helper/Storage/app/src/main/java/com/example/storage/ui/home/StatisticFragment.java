package com.example.storage.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.room.Room;

import com.example.storage.R;
import com.example.storage.data.DataContext;
import com.example.storage.data.models.StorageInGroups;
import com.example.storage.utils.DateUtils;
import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.components.Description;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;
import com.github.mikephil.charting.formatter.PercentFormatter;
import com.github.mikephil.charting.utils.ColorTemplate;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Фрагмент истории
 */
public class StatisticFragment extends Fragment {


    /**
     * Подключение к БД
     */
    DataContext dataContext;
    /**
     * Диаграмма
     */
    PieChart pieChart;
    /**
     * Кнопки
     */
    ImageView left;
    ImageView right;
    /**
     * Дата
     */
    Date date = DateUtils.today();
    /**
     * Вывод месяца
     */
    TextView month;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_statistic, container, false);
    }

    /**
     * Заполнение View
     */
    @Override
    public void onStart() {
        super.onStart();


        month = getView().findViewById(R.id.month);
        this.pieChart = getView().findViewById(R.id.charts);
        this.dataContext = Room.databaseBuilder(getContext(), DataContext.class, getString(R.string.database)).fallbackToDestructiveMigration().allowMainThreadQueries().build();
        pieChartDraw();

        this.left = getView().findViewById(R.id.left);
        left.setOnClickListener(v -> {
            date = DateUtils.addMonth(date, -1);
            pieChartDraw();
        });
        this.right = getView().findViewById(R.id.right);
        right.setOnClickListener(v -> {
            date = DateUtils.addMonth(date, 1);
            pieChartDraw();
        });
    }

    /**
     * Создание диаграммы
     */
    private void pieChartDraw() {
        month.setText(DateUtils.getName(date.getMonth()) + " " + (1900 + date.getYear()));

        ArrayList<PieEntry> entries = new ArrayList<>();
        List<StorageInGroups> storagePositionList = this.dataContext.getGroupWithStoragesDAO().getAllGroups();
        double count = storagePositionList.stream().map(x -> x.getStoragePositionList().stream().filter(z -> z.getDate().getMonth() == date.getMonth())
                .map(y -> y.getSum()).collect(Collectors.toList())).mapToDouble(x -> x.stream().mapToDouble(y -> y).sum()).sum();
        storagePositionList.stream().filter(x -> x.getStoragePositionList().size() > 0 && x.getStoragePositionList().stream().filter(z -> z.getDate().getMonth() == date.getMonth()).collect(Collectors.toList()).size() > 0)
                .forEach(k -> entries.add(new PieEntry((float) k.getStoragePositionList().stream().filter(z -> z.getDate().getMonth() == date.getMonth()).mapToDouble(x -> x.getSum()).sum() / (float) count * 100, k.getGroup().getName())));
        PieData pieData = new PieData();
        PieDataSet pieDataSet = new PieDataSet(entries, "");
        pieDataSet.setColors(ColorTemplate.VORDIPLOM_COLORS);
        pieData.setDataSet(pieDataSet);
        pieData.setValueFormatter(new PercentFormatter());
        Description description = pieChart.getDescription();
        description.setText("");
        pieChart.setData(pieData);
        pieChart.setDrawEntryLabels(false);
        pieChart.invalidate();
    }
}