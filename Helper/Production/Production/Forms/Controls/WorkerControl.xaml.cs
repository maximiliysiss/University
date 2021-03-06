﻿using Production.Forms.Controls.Models.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Production.Forms.Controls
{
    /// <summary>
    /// Interaction logic for WorkerControl.xaml
    /// </summary>
    public partial class WorkerControl : UserControl
    {
        public WorkerControl()
        {
            InitializeComponent();
            this.Reports.Content = new ReportList();
            this.Teams.Content = new WorkerTeamList(App.user);
        }
    }
}
