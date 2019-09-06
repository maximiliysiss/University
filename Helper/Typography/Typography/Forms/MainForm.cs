using System;
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
            {
                var listData = GlobalContext.FactoryGeneratorListForm.Build(tab.Name);
                tabControl1.SelectedIndexChanged += (s, e) =>
                {
                    MessageBox.Show("sdfsdf");
                };
                tab.Controls.AddRange(listData.Controls.Cast<Control>().ToArray());
            }
        }
    }
}
