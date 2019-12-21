using System.Linq;
using System.Windows;

namespace Childhood.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма ребенка для родителей
    /// </summary>
    public partial class ParentChildControl : Window
    {
        public ParentChildControl(Childhood.Models.Child child)
        {
            InitializeComponent();
            this.Checks.ItemsSource = App.Db.ChildChecks.Where(x => x.ChildId == child.ID).ToList();
            this.DataContext = child;
        }
    }
}
