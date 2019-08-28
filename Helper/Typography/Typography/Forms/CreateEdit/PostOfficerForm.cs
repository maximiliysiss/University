using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typography.Models;
using Typography.Services;

namespace Typography.Forms.CreateEdit
{
    /// <summary>
    /// Форма для создания/изменения PostOfficer
    /// </summary>
    public class PostOfficerForm : CreateEditBaseForm<Models.PostOfficer>
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;

        public PostOfficerForm(IDatabaseContext databaseContext, string name = null) : base(databaseContext, name)
        {
        }

        public PostOfficerForm(IDatabaseContext databaseContext, PostOfficer elem, string name = null)
            : base(databaseContext, elem, name)
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
            this.label1.Location = new System.Drawing.Point(22, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(89, 27);
            this.textBox1.Name = "PostOfficerName";
            this.textBox1.Size = new System.Drawing.Size(255, 20);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(89, 81);
            this.textBox2.Name = "PostOfficerNumber";
            this.textBox2.Size = new System.Drawing.Size(255, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Номер";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(89, 141);
            this.textBox3.Name = "PostOfficerAdress";
            this.textBox3.Size = new System.Drawing.Size(255, 20);
            this.textBox3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Адрес";
            // 
            // actionBtn
            // 
            this.actionBtn.Location = new System.Drawing.Point(25, 184);
            this.actionBtn.Size = new System.Drawing.Size(319, 23);
            this.actionBtn.TabIndex = 6;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(25, 228);
            this.deleteBtn.Size = new System.Drawing.Size(319, 23);
            this.deleteBtn.TabIndex = 7;
            // 
            // PostWorkForm
            // 
            this.ClientSize = new System.Drawing.Size(372, 263);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "PostWorkForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
