namespace BookLibrary
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
            this.addNewBook = new System.Windows.Forms.Button();
            this.bookDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.bookDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // addNewBook
            // 
            this.addNewBook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addNewBook.Location = new System.Drawing.Point(12, 415);
            this.addNewBook.Name = "addNewBook";
            this.addNewBook.Size = new System.Drawing.Size(660, 23);
            this.addNewBook.TabIndex = 1;
            this.addNewBook.Text = "Add new book";
            this.addNewBook.UseVisualStyleBackColor = true;
            this.addNewBook.Click += new System.EventHandler(this.addNewBook_Click);
            // 
            // bookDataGridView
            // 
            this.bookDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bookDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bookDataGridView.Location = new System.Drawing.Point(12, 12);
            this.bookDataGridView.MultiSelect = false;
            this.bookDataGridView.Name = "bookDataGridView";
            this.bookDataGridView.ReadOnly = true;
            this.bookDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bookDataGridView.Size = new System.Drawing.Size(660, 397);
            this.bookDataGridView.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 450);
            this.Controls.Add(this.bookDataGridView);
            this.Controls.Add(this.addNewBook);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.bookDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addNewBook;
        public System.Windows.Forms.DataGridView bookDataGridView;
    }
}

