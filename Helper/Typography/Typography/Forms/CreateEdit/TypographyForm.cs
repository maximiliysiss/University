using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typography.Models;
using Typography.Services;

namespace Typography.Forms.CreateEdit
{
    public class TypographyForm : CreateEditBaseForm<Models.Typography>
    {
        public TypographyForm(IDatabaseContext databaseContext, Func<List<Models.Typography>> dbSet, string name = null) : base(databaseContext, dbSet, name)
        {
        }

        public TypographyForm(Models.Typography elem, Func<List<Models.Typography>> dbSet, string name = null) : base(elem, dbSet, name)
        {
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            this.SuspendLayout();
            // 
            // TypographyForm
            // 
            this.ClientSize = new System.Drawing.Size(509, 516);
            this.Name = "TypographyForm";
            this.ResumeLayout(false);

        }
    }
}
