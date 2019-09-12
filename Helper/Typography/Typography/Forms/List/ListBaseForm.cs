using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Typography.Forms.CreateEdit;
using Typography.Services;
using Typography.Services.Templates;
using static System.Windows.Forms.Control;

namespace Typography.Forms.List
{
    /// <summary>
    /// Интерфейс для форм с возможностью выбрать объект
    /// </summary>
    public interface ISelectForm
    {
        /// <summary>
        /// Выбранный объект
        /// </summary>
        object SelectedContext { get; set; }
        /// <summary>
        /// Отобразить окно
        /// </summary>
        /// <returns></returns>
        DialogResult ShowDialog();
    }

    public interface IRefreshDataForm
    {
        void RefreshData();
        ControlCollection Controls { get; }
    }

    /// <summary>
    /// Форма для отображения списка объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ListBaseForm<T> : Form, ISelectForm, IRefreshDataForm where T : class, new()
    {
        /// <summary>
        /// Таблица
        /// </summary>
        private DataGridView dataGridView;
        /// <summary>
        /// БД
        /// </summary>
        private readonly IDatabaseContext databaseContext;
        /// <summary>
        /// Кнопка добавить элемент
        /// </summary>
        private Button addNewElement;
        /// <summary>
        /// Таблица данных из БД
        /// </summary>
        private readonly DbSet<T> actionList;
        /// <summary>
        /// Это форма для выбора
        /// </summary>
        public bool IsSelect { get; set; } = false;
        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public object SelectedContext { get; set; }
        /// <summary>
        /// Тип объекта
        /// </summary>
        private Type type = typeof(T);
        /// <summary>
        /// Layout для элементов
        /// </summary>
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBox1;
        private Button reportBtn;

        /// <summary>
        /// Сохраненные элементы (кэш)
        /// </summary>
        private List<T> casheElements;
        /// <summary>
        /// Обновить данные
        /// </summary>
        public void RefreshData() => dataGridView.DataSource = casheElements = actionList.ToList();

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <param name="actionList"></param>
        /// <param name="name"></param>
        public ListBaseForm(IDatabaseContext databaseContext, DbSet<T> actionList, string name = null)
        {
            this.actionList = actionList;
            this.databaseContext = databaseContext;
            InitializeComponent();
            this.Name = name ?? type.Name;
            RefreshData();
        }

        /// <summary>
        /// При двойном клике на строку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsSelect)
            {
                SelectedContext = casheElements[e.RowIndex];
                this.Close();
            }
            else
            {
                var dialog = GlobalContext.FactoryGetEditForm.BuildWithArguments(casheElements[e.RowIndex], this.Name) as CreateEditBaseForm<T>;
                dialog.IsEdit = true;
                dialog.ShowDialog();
                RefreshData();
            }
        }

        /// <summary>
        /// Создать элементы
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.addNewElement = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.reportBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView, 2);
            this.dataGridView.Location = new System.Drawing.Point(3, 48);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(734, 311);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            // 
            // addNewElement
            // 
            this.addNewElement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.addNewElement, 2);
            this.addNewElement.Location = new System.Drawing.Point(3, 365);
            this.addNewElement.Name = "addNewElement";
            this.addNewElement.Size = new System.Drawing.Size(734, 39);
            this.addNewElement.TabIndex = 1;
            this.addNewElement.Text = "Добавить элемент";
            this.addNewElement.UseVisualStyleBackColor = true;
            this.addNewElement.Click += new System.EventHandler(this.addNewElement_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.addNewElement, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.reportBtn, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(740, 453);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Margin = new System.Windows.Forms.Padding(12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(716, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.SearchTextChanged);
            // 
            // reportBtn
            // 
            this.reportBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportBtn.Location = new System.Drawing.Point(3, 410);
            this.reportBtn.Name = "reportBtn";
            this.reportBtn.Size = new System.Drawing.Size(734, 40);
            this.reportBtn.TabIndex = 4;
            this.reportBtn.Text = "Создать отчет";
            this.reportBtn.UseVisualStyleBackColor = true;
            this.reportBtn.Click += new System.EventHandler(this.ReportBtn_Click);
            // 
            // ListBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(740, 453);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ListBaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Нажатие на кнопку добавления нового элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewElement_Click(object sender, EventArgs e)
        {
            var dialog = GlobalContext.FactoryGeneratorCreateForm.Build(this.Name) as CreateEditBaseForm<T>;
            dialog.IsEdit = false;
            dialog.ShowDialog();
            RefreshData();
        }

        protected abstract bool Search(T elem, string val);

        private void SearchTextChanged(object sender, EventArgs e) => dataGridView.DataSource = casheElements = actionList.Where(x => string.IsNullOrEmpty(textBox1.Text) ? true : Search(x, textBox1.Text)).ToList();

        private void ReportBtn_Click(object sender, EventArgs e)
        {
            var reportForm = new ReportForm();
            reportForm.ReportColumns = this.dataGridView.Columns.Cast<DataGridViewColumn>()
                      .Select(x => new ReportColumn(x.DataPropertyName)
                      { Title = x.HeaderText, Width = x.Width }).ToList();
            reportForm.ReportData = this.dataGridView.DataSource;
            reportForm.ShowDialog();
        }
    }
}
