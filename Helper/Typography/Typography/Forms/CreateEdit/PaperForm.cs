using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Typography.Models;
using Typography.Services;

namespace Typography.Forms.CreateEdit
{
    /// <summary>
    /// Форма для создания/изменения Paper
    /// </summary>
    public class PaperForm : CreateEditBaseForm<Paper>
    {
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox5;
        private Label label5;

        public PaperForm(IDatabaseContext databaseContext, string name = null) : base(databaseContext, name)
        {
        }

        public PaperForm(IDatabaseContext databaseContext, Paper elem, string name = null) : base(databaseContext, elem, name)
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
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 13);
            this.textBox1.Name = "PaperName";
            this.textBox1.Size = new System.Drawing.Size(281, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(115, 51);
            this.textBox2.Name = "PaperPrice";
            this.textBox2.Size = new System.Drawing.Size(281, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Цена";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(115, 91);
            this.textBox3.Name = "EditorFIO";
            this.textBox3.Size = new System.Drawing.Size(281, 20);
            this.textBox3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "ФИО редактора";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(115, 134);
            this.textBox4.Name = "EditionCode";
            this.textBox4.Size = new System.Drawing.Size(281, 20);
            this.textBox4.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Код изменения";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(115, 175);
            this.textBox5.Name = "PaperQuantity";
            this.textBox5.Size = new System.Drawing.Size(281, 20);
            this.textBox5.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Количество страниц";
            // 
            // actionBtn
            // 
            this.actionBtn.Location = new System.Drawing.Point(13, 218);
            this.actionBtn.Size = new System.Drawing.Size(383, 23);
            this.actionBtn.TabIndex = 10;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(12, 257);
            this.deleteBtn.Size = new System.Drawing.Size(383, 23);
            this.deleteBtn.TabIndex = 11;
            // 
            // PaperForm
            // 
            this.ClientSize = new System.Drawing.Size(408, 294);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "PaperForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
