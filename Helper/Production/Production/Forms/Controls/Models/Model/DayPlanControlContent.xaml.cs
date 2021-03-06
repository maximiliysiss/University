﻿using Microsoft.EntityFrameworkCore;
using Production.Models;
using System.Linq;
using System.Windows.Controls;

namespace Production.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма плана на день
    /// </summary>
    public class DayPlanControl : BaseModelControl<DayPlan>
    {
        public DayPlanControl(DayPlan obj) : base(obj, new DayPlanControlContent(obj))
        {
        }

        /// <summary>
        /// Форма для изменения
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool IsEdit(DayPlan obj) => obj.ID != 0;
    }

    /// <summary>
    /// Interaction logic for DayPlanControlContent.xaml
    /// </summary>
    public partial class DayPlanControlContent : UserControl
    {
        public DayPlanControlContent(DayPlan obj)
        {
            InitializeComponent();
            this.Schedules.ItemsSource = App.Db.Schedules.Include(x=>x.Team).ToList();
            this.Details.ItemsSource = App.Db.Details.ToList();
            this.DataContext = obj;
        }
    }
}
