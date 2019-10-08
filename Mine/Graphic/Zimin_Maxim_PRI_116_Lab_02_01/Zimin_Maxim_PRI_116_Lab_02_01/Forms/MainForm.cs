using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zimin_Maxim_PRI_116_Lab_02_01.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ну, что сказать!");
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            Random random = new Random(Environment.TickCount);
            var size = Screen.PrimaryScreen.WorkingArea.Size;
            var windowSize = this.Size;
            this.Location = new Point(random.Next() % (size.Width - windowSize.Width),
                random.Next() % (size.Height - windowSize.Height));
        }
    }
}
