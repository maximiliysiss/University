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
    /// Форма для создания/изменения Release
    /// </summary>
    public class ReleaseForm : CreateEditBaseForm<Models.Release>
    {
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;

        public ReleaseForm(IDatabaseContext databaseContext, string name = null) : base(databaseContext, name)
        {
        }

        public ReleaseForm(IDatabaseContext databaseContext, Release elem, string name = null) : base(databaseContext, elem, name)
        {
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Типография";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(99, 13);
            this.textBox1.Name = "Typography";
            this.textBox1.Size = new System.Drawing.Size(277, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(99, 52);
            this.textBox2.Name = "Paper";
            this.textBox2.Size = new System.Drawing.Size(277, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Документ";
            // 
            // actionBtn
            // 
            this.actionBtn.Location = new System.Drawing.Point(16, 94);
            this.actionBtn.Size = new System.Drawing.Size(360, 23);
            this.actionBtn.TabIndex = 4;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(16, 135);
            this.deleteBtn.Size = new System.Drawing.Size(360, 23);
            this.deleteBtn.TabIndex = 5;
            // 
            // ReleaseForm
            // 
            this.ClientSize = new System.Drawing.Size(388, 168);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "ReleaseForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
