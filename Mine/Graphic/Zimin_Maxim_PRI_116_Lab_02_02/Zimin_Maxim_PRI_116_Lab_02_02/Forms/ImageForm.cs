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
    public partial class ImageForm : Form
    {
        public ImageForm(Image image)
        {
            InitializeComponent();
            this.pictureBox1.Size = image.Size;
            this.pictureBox1.Image = image;
        }
    }
}
