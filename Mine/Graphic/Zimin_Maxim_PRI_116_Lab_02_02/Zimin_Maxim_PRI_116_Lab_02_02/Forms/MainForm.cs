using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zimin_Maxim_PRI_116_Lab_02_02.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.newWindowToolStripMenuItem.Image = Image.FromFile(@"Files/newfile-logo.jpg");
            this.iMGToolStripMenuItem.Image = Image.FromFile(@"Files/jpg-logo.png");
            this.pNGToolStripMenuItem.Image = Image.FromFile(@"Files/png-logo.png");
            openFileDialog1.Multiselect = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
                new ImageForm(pictureBox1.Image).ShowDialog();
        }

        private void PNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "png|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                pictureBox1.ImageLocation = filePath;
            }
        }

        private void IMGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "jpg|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                pictureBox1.ImageLocation = filePath;
            }
        }

        private void AboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
    }
}
