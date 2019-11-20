using Chemical.Models;
using Chemical.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Chemical.Forms.Controls
{
    /// <summary>
    /// Форма прораба
    /// </summary>
    public partial class ProWorkerControl : UserControl
    {
        /// <summary>
        /// Подключение к БД
        /// </summary>
        private DatabaseContext db = App.ChemicalModules.Resolve<DatabaseContext>();

        public ProWorkerControl()
        {
            InitializeComponent();
            Refresh();
        }

        /// <summary>
        /// Обновить
        /// </summary>
        public void Refresh()
        {
            GridPlans.ItemsSource = db.Plans.Where(x => !x.IsExercised).Include(x => x.Material).Include(x => x.Stock).ToList();
        }

        /// <summary>
        /// Двойной клик по строке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var res = MessageBox.Show("План выполнен?", "Решение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                var context = (sender as DataGridRow).DataContext as Plan;
                context.IsExercised = true;
                context.PlanExecute = DateTime.Now;
                db.Add(new MaterialInStock { Count = context.Count, MaterialId = context.MaterialId, StockId = context.StockId });
                db.Update(context);
                db.SaveChanges();
                Refresh();
            }
        }
    }
}
