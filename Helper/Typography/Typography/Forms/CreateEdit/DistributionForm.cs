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
    public class DistributionForm : CreateEditBaseForm<Models.Distribution>
    {
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;

        public DistributionForm(IDatabaseContext databaseContext, DbSet<Distribution> dbSet, string name = null) : base(databaseContext, dbSet, name)
        {
        }

        public DistributionForm(IDatabaseContext databaseContext, Distribution elem, DbSet<Distribution> dbSet, string name = null) : base(databaseContext, elem, dbSet, name)
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
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paper";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(108, 13);
            this.textBox1.Name = "Paper";
            this.textBox1.Size = new System.Drawing.Size(250, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(108, 58);
            this.textBox2.Name = "PostOfficer";
            this.textBox2.Size = new System.Drawing.Size(250, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "PostOfficer";
            // 
            // actionBtn
            // 
            this.actionBtn.Location = new System.Drawing.Point(13, 95);
            this.actionBtn.Size = new System.Drawing.Size(345, 23);
            this.actionBtn.TabIndex = 4;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(13, 133);
            this.deleteBtn.Size = new System.Drawing.Size(345, 23);
            this.deleteBtn.TabIndex = 5;
            // 
            // DistributionForm
            // 
            this.ClientSize = new System.Drawing.Size(382, 175);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "DistributionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
