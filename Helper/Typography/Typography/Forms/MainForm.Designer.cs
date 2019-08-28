namespace Typography.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Release = new System.Windows.Forms.TabPage();
            this.PostOfficer = new System.Windows.Forms.TabPage();
            this.Paper = new System.Windows.Forms.TabPage();
            this.Distribution = new System.Windows.Forms.TabPage();
            this.Typography = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Release);
            this.tabControl1.Controls.Add(this.PostOfficer);
            this.tabControl1.Controls.Add(this.Paper);
            this.tabControl1.Controls.Add(this.Distribution);
            this.tabControl1.Controls.Add(this.Typography);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // Release
            // 
            this.Release.Location = new System.Drawing.Point(4, 22);
            this.Release.Name = "Release";
            this.Release.Padding = new System.Windows.Forms.Padding(3);
            this.Release.Size = new System.Drawing.Size(792, 424);
            this.Release.TabIndex = 0;
            this.Release.Text = "Release";
            this.Release.UseVisualStyleBackColor = true;
            // 
            // PostOfficer
            // 
            this.PostOfficer.Location = new System.Drawing.Point(4, 22);
            this.PostOfficer.Name = "PostOfficer";
            this.PostOfficer.Padding = new System.Windows.Forms.Padding(3);
            this.PostOfficer.Size = new System.Drawing.Size(792, 424);
            this.PostOfficer.TabIndex = 1;
            this.PostOfficer.Text = "Post Officer";
            this.PostOfficer.UseVisualStyleBackColor = true;
            // 
            // Paper
            // 
            this.Paper.Location = new System.Drawing.Point(4, 22);
            this.Paper.Name = "Paper";
            this.Paper.Size = new System.Drawing.Size(792, 424);
            this.Paper.TabIndex = 2;
            this.Paper.Text = "Paper";
            this.Paper.UseVisualStyleBackColor = true;
            // 
            // Distribution
            // 
            this.Distribution.Location = new System.Drawing.Point(4, 22);
            this.Distribution.Name = "Distribution";
            this.Distribution.Size = new System.Drawing.Size(792, 424);
            this.Distribution.TabIndex = 3;
            this.Distribution.Text = "Distribution";
            this.Distribution.UseVisualStyleBackColor = true;
            // 
            // Typography
            // 
            this.Typography.Location = new System.Drawing.Point(4, 22);
            this.Typography.Name = "Typography";
            this.Typography.Size = new System.Drawing.Size(792, 424);
            this.Typography.TabIndex = 4;
            this.Typography.Text = "Typography";
            this.Typography.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Release;
        private System.Windows.Forms.TabPage PostOfficer;
        private System.Windows.Forms.TabPage Paper;
        private System.Windows.Forms.TabPage Distribution;
        private System.Windows.Forms.TabPage Typography;
    }
}