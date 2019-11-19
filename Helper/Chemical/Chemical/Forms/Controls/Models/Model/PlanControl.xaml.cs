using Chemical.Models;
using Chemical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chemical.Forms.Controls.Models.Model
{
    public class PlanControl : BaseModelControl<Plan>
    {
        public PlanControl(Plan obj) : base(obj, new PlanControlContent(obj))
        {
            Title = "План выпуска";

            if (obj.IsExercised)
            {
                DeleteBtn.Visibility = Visibility.Hidden;
                Action.Visibility = Visibility.Hidden;
            }
        }

        public override bool IsEdit(Plan obj) => obj.ID != 0;
    }


    /// <summary>
    /// Interaction logic for PlanControl.xaml
    /// </summary>
    public partial class PlanControlContent : UserControl
    {
        public PlanControlContent(Plan plan)
        {
            var db = App.ChemicalModules.Resolve<DatabaseContext>();

            InitializeComponent();
            this.RawMaterials.ItemsSource = db.RawMaterials.ToList();
            this.Stocks.ItemsSource = db.Stocks.ToList();
            this.DataContext = plan;
        }
    }
}
