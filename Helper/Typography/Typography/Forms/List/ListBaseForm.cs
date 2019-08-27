using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Typography.Forms.CreateEdit;
using Typography.Services;

namespace Typography.Forms.List
{
    public class ListBaseForm<T> : Form where T : class, new()
    {
        private DataGridView dataGridView;
        private readonly IDatabaseContext databaseContext;
        private Button addNewElement;
        private readonly DbSet<T> actionList;

        private Type type = typeof(T);

        private List<T> casheElements;
        public void RefreshData() => dataGridView.DataSource = casheElements = actionList.ToList();

        public ListBaseForm(IDatabaseContext databaseContext, DbSet<T> actionList, string name = null)
        {
            this.actionList = actionList;
            this.databaseContext = databaseContext;
            InitializeComponent();
            this.Name = name ?? type.Name;
            RefreshData();
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dialog = GlobalContext.FactoryGeneratorCreateEdit.Build(this.Name) as CreateEditBaseForm<T>;
            dialog.Context = casheElements[e.RowIndex];
            dialog.IsEdit = true;
            dialog.ShowDialog();
            RefreshData();
        }

        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.addNewElement = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right);
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(13, 13);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(715, 409);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
            // 
            // addNewElement
            // 
            this.addNewElement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addNewElement.Location = new System.Drawing.Point(13, 424);
            this.addNewElement.Name = "addNewElement";
            this.addNewElement.Size = new System.Drawing.Size(162, 23);
            this.addNewElement.TabIndex = 1;
            this.addNewElement.Text = "Add new";
            this.addNewElement.UseVisualStyleBackColor = true;
            this.addNewElement.Click += new System.EventHandler(this.addNewElement_Click);
            // 
            // ListBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(740, 453);
            this.Controls.Add(this.addNewElement);
            this.Controls.Add(this.dataGridView);
            this.Name = "ListBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        private void addNewElement_Click(object sender, EventArgs e)
        {
            var dialog = GlobalContext.FactoryGeneratorCreateEdit.Build(this.Name) as CreateEditBaseForm<T>;
            dialog.IsEdit = false;
            dialog.ShowDialog();
            RefreshData();
        }
    }
}
