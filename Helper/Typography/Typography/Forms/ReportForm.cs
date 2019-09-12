using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Typography.Services.Templates;

namespace Typography.Forms
{
    /// <summary>
    /// Форма отчета
    /// </summary>
    public class ReportForm : Form
    {

        public ReportForm()
        {
            InitializeComponent();
            ReportColumns = new List<ReportColumn>();
            this.Load += new EventHandler(ReportForm_Load);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.reportViewer1 = new ReportViewer();
            reportViewer1.Dock = DockStyle.Fill;
            // 
            // ReportForm
            // 
            this.ClientSize = new System.Drawing.Size(636, 415);
            this.Name = "ReportForm";
            this.ResumeLayout(false);
            this.Controls.Add(reportViewer1);
        }

        public ReportViewer reportViewer1;
        public List<ReportColumn> ReportColumns { get; set; }
        public object ReportData { get; set; }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            var report = new GenerateOrder();
            report.Session = new Dictionary<string, object>();
            report.Session["Model"] = this.ReportColumns;
            report.Initialize();
            var rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", this.ReportData);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            var reportContent = System.Text.Encoding.UTF8.GetBytes(report.TransformText());
            using (var stream = new System.IO.MemoryStream(reportContent))
            {
                this.reportViewer1.LocalReport.LoadReportDefinition(stream);
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
