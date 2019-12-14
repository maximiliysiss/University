using Microsoft.EntityFrameworkCore;
using Production.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Production.Forms.Controls.Models.Model
{
    public class ReportControl : BaseModelControl<Report>
    {
        public ReportControl(Report obj) : base(obj, new ReportControlContent(obj))
        {
        }

        public override bool IsEdit(Report obj) => obj.ID != 0;

        protected override bool PrevAction(Report obj)
        {
            obj.UserId = App.user.ID;
            return base.PrevAction(obj);
        }
    }

    /// <summary>
    /// Interaction logic for ReportControlContent.xaml
    /// </summary>
    public partial class ReportControlContent : UserControl
    {
        public ReportControlContent(Report obj)
        {
            InitializeComponent();
            DayPlans.ItemsSource = App.Db.DayPlans.Include(x => x.Detail).Include(x => x.Schedule).ThenInclude(x => x.Team)
                .Where(x => x.Schedule.Team.Users.Any(y => y.WorkerId == App.user.ID)).ToList();
            this.DataContext = obj;
        }
    }
}
