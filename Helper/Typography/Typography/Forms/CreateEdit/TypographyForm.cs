using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Typography.Models;
using Typography.Services;

namespace Typography.Forms.CreateEdit
{
    public class TypographyForm : CreateEditBaseForm<Models.Typography>
    {
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
        private Label label3;

        public TypographyForm(IDatabaseContext databaseContext, DbSet<Models.Typography> dbSet, string name = null) : base(databaseContext, dbSet, name)
        {
        }

        public TypographyForm(IDatabaseContext databaseContext, Models.Typography elem, DbSet<Models.Typography> dbSet, string name = null)
            : base(databaseContext, elem, dbSet, name)
        {
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 13);
            this.textBox1.Name = "TypographyName";
            this.textBox1.Size = new System.Drawing.Size(282, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(87, 54);
            this.textBox2.Name = "TypographyNumber";
            this.textBox2.Size = new System.Drawing.Size(282, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Number";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(87, 98);
            this.textBox3.Name = "TypographyAdress";
            this.textBox3.Size = new System.Drawing.Size(282, 20);
            this.textBox3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Adress";
            // 
            // actionBtn
            // 
            this.actionBtn.Location = new System.Drawing.Point(16, 142);
            this.actionBtn.Size = new System.Drawing.Size(353, 23);
            this.actionBtn.TabIndex = 6;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(16, 172);
            this.deleteBtn.Size = new System.Drawing.Size(353, 23);
            this.deleteBtn.TabIndex = 7;
            // 
            // TypographyForm
            // 
            this.ClientSize = new System.Drawing.Size(386, 207);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "TypographyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
