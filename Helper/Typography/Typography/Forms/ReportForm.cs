using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Typography.Services.Templates;

namespace Typography.Forms
{
    public class ReportForm : Form
    {
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

        public ReportForm()
        {
            InitializeComponent();
            ReportColumns = new List<ReportColumn>();
            this.Load += new EventHandler(ReportForm_Load);
        }

        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.Size = new System.Drawing.Size(396, 246);
            this.reportViewer1.TabIndex = 0;
            // 
            // ReportForm
            // 
            this.ClientSize = new System.Drawing.Size(636, 415);
            this.Name = "ReportForm";
            this.ResumeLayout(false);

        }

        public List<ReportColumn> ReportColumns { get; set; }
        public Object ReportData { get; set; }

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
