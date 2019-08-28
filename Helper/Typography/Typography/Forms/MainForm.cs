﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Typography.Services;

namespace Typography.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            foreach (var tab in tabControl1.TabPages.Cast<TabPage>())
                tab.Controls.AddRange(GlobalContext.FactoryGeneratorListForm.Build(tab.Name).Controls.Cast<Control>().ToArray());
        }
    }
}
